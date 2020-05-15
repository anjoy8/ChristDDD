git pull;
rm -rf .PublishFiles;
dotnet build;
dotnet publish -o /home/ChristDDD/Christ3D.UI.Web/bin/Debug/netcoreapp3.1;
cp -r /home/ChristDDD/Christ3D.UI.Web/bin/Debug/netcoreapp3.1 .PublishFiles;
echo "Successfully!!!! ^ please see the file .PublishFiles";