[CmdletBinding(DefaultParameterSetName = "no-arguments")]
Param (
    [Parameter(HelpMessage = "Alternative login using app client.",
        ParameterSetName = "by-pass")]
    [bool]$ByPass = $false
)

$topologyArray = "xp0", "xp1", "xm1";

$ErrorActionPreference = "Stop";
$startDirectory = ".\run\sitecore-";
$workingDirectoryPath;
$envCheck;
# Double check whether init has been run
$envCheckVariable = "HOST_LICENSE_FOLDER";

# Ensure windows container engine is turned on
Start-Process -Wait -NoNewWindow cmd -ArgumentList /c, "DockerCli.exe -SwitchWindowsEngine" -WorkingDirectory "C:\Program Files\Docker\Docker\"

# Kill IIS if it is running
try { IISRESET /STOP }
catch { Write-Host "IISReset failed or IIS not installed" -ForegroundColor Yellow }

# Check port numbers
function Test-SitecorePorts {
    param (
        [string[]]$portnumbers
    )

    foreach ($portnumber in $portnumbers) {
        $result = test-netconnection -computername 127.0.0.1 -port $portnumber -ErrorAction SilentlyContinue -WarningAction SilentlyContinue
        if($result.TcpTestSucceeded)
        {
            Write-Error "Port $portnumber is in use and should be available for Sitecore on docker."
        }
        else
        {
            Write-Host "Port $portnumber available..." -ForegroundColor Green
        }
    }
}

Test-SitecorePorts 443, 8079, 14430, 8081, 8984, 8983

# Check docker is running
try {
    # Try and check the service status (sometimes the service isn't listed at all)
    $arrService = Get-Service -Name "Docker Engine"
    if ($arrService.Status -ne 'Running') {
        Write-Error "Docker for Windows is not running."
    }
} catch {
    # You can get an error if the service doesn't exist which can happen if the gui isn't running
    Write-Error "Docker for Windows is not running."
}


foreach ($topology in $topologyArray)
{
  $envCheck = Get-Content (Join-Path -Path ($startDirectory + $topology) -ChildPath .env) -Encoding UTF8 | Where-Object { $_ -imatch "^$envCheckVariable=.+" }
  if ($envCheck) {
    $workingDirectoryPath = $startDirectory + $topology;
    break
  }
}

if (-not $envCheck) {
    throw "$envCheckVariable does not have a value. Did you run 'init.ps1 -InitEnv'?"
}
Push-Location $workingDirectoryPath

# Build all containers in the Sitecore instance, forcing a pull of latest base containers
Write-Host "Building containers..." -ForegroundColor Green
docker-compose build
if ($LASTEXITCODE -ne 0) {
    Write-Error "Container build failed, see errors above."
}

# Start the Sitecore instance
Write-Host "Starting Sitecore environment..." -ForegroundColor Green
docker-compose up -d

Pop-Location
# Wait for Traefik to expose CM route
Write-Host "Waiting for CM to become available..." -ForegroundColor Green
$startTime = Get-Date
do {
    Start-Sleep -Milliseconds 100
    try {
        $status = Invoke-RestMethod "http://localhost:8079/api/http/routers/cm-secure@docker"
    } catch {
        if ($_.Exception.Response.StatusCode.value__ -ne "404") {
            throw
        }
    }
} while ($status.status -ne "enabled" -and $startTime.AddSeconds(15) -gt (Get-Date))
if (-not $status.status -eq "enabled") {
    $status
    Write-Error "Timeout waiting for Sitecore CM to become available via Traefik proxy. Check CM container logs."
}
if ($ByPass) {
  dotnet sitecore login --cm https://cm.dotnetcore_xp_sxa_demo.localhost/ --auth https://id.dotnetcore_xp_sxa_demo.localhost/ --allow-write true --client-id "SitecoreCLIServer" --client-secret "testsecret" --client-credentials true
}else {
  dotnet sitecore login --cm https://cm.dotnetcore_xp_sxa_demo.localhost/ --auth https://id.dotnetcore_xp_sxa_demo.localhost/ --allow-write true
}

if ($LASTEXITCODE -ne 0) {
    Write-Error "Unable to log into Sitecore, did the Sitecore environment start correctly? See logs above."
}

# Populate Solr managed schemas to avoid errors during item push
Write-Host "Populating Solr managed schema..." -ForegroundColor Green
dotnet sitecore index schema-populate

Write-Host "Pushing latest items to Sitecore..." -ForegroundColor Green

dotnet sitecore ser push --publish
if ($LASTEXITCODE -ne 0) {
    Write-Error "Serialization push failed, see errors above."
}

Write-Host "Opening site..." -ForegroundColor Green

Start-Process https://cm.dotnetcore_xp_sxa_demo.localhost/sitecore/
Start-Process https://www.dotnetcore_xp_sxa_demo.localhost/

Write-Host ""
Write-Host "Use the following command to monitor your Rendering Host:" -ForegroundColor Green
Write-Host "docker-compose logs -f rendering"
Write-Host ""
