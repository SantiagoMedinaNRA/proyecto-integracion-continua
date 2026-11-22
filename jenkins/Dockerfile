FROM mcr.microsoft.com/dotnet/sdk:10.0 AS dotnet-base

FROM jenkins/jenkins:lts

USER root

# .NET SDK copiado completo desde la imagen oficial (legible por el usuario jenkins)
COPY --from=dotnet-base /usr/share/dotnet /usr/share/dotnet
RUN ln -s /usr/share/dotnet/dotnet /usr/local/bin/dotnet

# Cliente Docker y plugins compose/buildx copiados desde la imagen oficial docker:cli
COPY --from=docker:cli /usr/local/bin/docker /usr/local/bin/docker
COPY --from=docker:cli /usr/local/libexec/docker/cli-plugins /usr/local/libexec/docker/cli-plugins

RUN groupadd docker || true && usermod -aG docker jenkins

COPY plugins.txt /usr/share/jenkins/ref/plugins.txt
RUN jenkins-plugin-cli --plugin-file /usr/share/jenkins/ref/plugins.txt

COPY jenkins.yaml /usr/share/jenkins/ref/jenkins.yaml
ENV CASC_JENKINS_CONFIG=/usr/share/jenkins/ref/jenkins.yaml
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
# Sin libicu en la imagen: .NET corre en modo de globalización invariante
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1

USER jenkins
