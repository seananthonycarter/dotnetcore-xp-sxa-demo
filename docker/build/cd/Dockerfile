# escape=`

ARG PARENT_IMAGE
ARG SOLUTION_IMAGE
ARG TOOLS_IMAGE
ARG HEADLESS_SERVICES_IMAGE
ARG SXA_IMAGE

FROM ${SOLUTION_IMAGE} as solution
FROM ${TOOLS_IMAGE} as tools
FROM ${HEADLESS_SERVICES_IMAGE} AS headless_services
FROM ${SXA_IMAGE} as sxa
FROM ${PARENT_IMAGE}

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

WORKDIR C:\inetpub\wwwroot

# Copy developer tools and entrypoint
COPY --from=tools C:\tools C:\tools

# Copy and init the JSS / Headless Services Module
COPY --from=headless_services C:\module\cd\content C:\inetpub\wwwroot
COPY --from=headless_services C:\module\tools C:\module\tools
RUN C:\module\tools\Initialize-Content.ps1 -TargetPath C:\inetpub\wwwroot; `
    Remove-Item -Path C:\module -Recurse -Force;

# Copy solution website files
COPY --from=solution /artifacts/sitecore/ ./
#COPY .\dotnetcore-xp-sxa-demo.config c:\inetpub\wwwroot\App_Config\Include
#COPY .\dotnetcore-xp-sxa-demo-cd.config c:\inetpub\wwwroot\App_Config\Include


# Add SXA module
#COPY --from=sxa \module\cd\content .\
#COPY --from=sxa \module\tools \module\tools
#RUN C:\module\tools\Initialize-Content.ps1 -TargetPath .\; `
#    Remove-Item -Path C:\module -Recurse -Force;

COPY --from=sxa C:\module\cd\content C:\inetpub\wwwroot
COPY --from=sxa C:\module\tools C:\module\tools
RUN C:\module\tools\Initialize-Content.ps1 -TargetPath C:\inetpub\wwwroot; `
  Remove-Item -Path C:\module -Recurse -Force;

# Copy solution transforms
#COPY --from=solution \artifacts\transforms\ \transforms\solution\

# Perform solution transforms
#RUN C:\tools\scripts\Invoke-XdtTransform.ps1 -Path .\ -XdtPath C:\transforms\solution\DockerExamples.Website

# Copy solution transforms
#COPY --from=solution \artifacts\transforms\ \transforms\solution\

# Copy role transforms
COPY .\transforms\ \transforms\role\

# Perform solution transforms
#RUN C:\tools\scripts\Invoke-XdtTransform.ps1 -Path .\ -XdtPath C:\transforms\solution\rendering

# Perform role transforms
RUN C:\tools\scripts\Invoke-XdtTransform.ps1 -Path .\ -XdtPath C:\transforms\role
