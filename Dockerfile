FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS builder

COPY ./src /opt/build

WORKDIR /opt/build
RUN dotnet publish -c Release -r linux-x64

FROM mcr.microsoft.com/dotnet/runtime-deps:5.0-buster-slim

COPY --from=builder /opt/build/bin/Release/net5.0/linux-x64/publish/ /app/

WORKDIR /opt
ENV TZ=Asia/Shanghai
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

CMD [ "/app/cqhttp.Cyan.Demo" ]
VOLUME [ "/opt" ]
