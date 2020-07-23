FROM swr.cn-south-1.myhuaweicloud.com/mcr/aspnet:3.1-alpine
RUN ln -sf /usr/share/zoneinfo/Asia/Shanghai /etc/localtime
RUN echo 'Asia/Shanghai' >/etc/timezone
WORKDIR /app
COPY . . 
EXPOSE 4773 
ENTRYPOINT ["dotnet", "Christ3D.UI.Web.dll","-b","0.0.0.0"]