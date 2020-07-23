# 停止容器
docker stop dddcontainer
# 删除容器
docker rm dddcontainer
# 删除镜像
docker rmi laozhangisphi/dddimg
# 切换目录
cd /home/ChristDDD/
# 发布项目
./Christ3D.Publish.Linux.sh
# 进入目录
cd /home/ChristDDD/.PublishFiles
# 编译镜像
docker build -t laozhangisphi/dddimg .
# 生成容器
docker run --name=dddcontainer -v /etc/localtime:/etc/localtime -it -p 4773:4773 laozhangisphi/dddimg
# 启动容器
docker start dddcontainer
