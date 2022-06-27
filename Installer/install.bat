@echo off
echo -^> -----Installation STARTED-----
@echo off
echo -^> Installing RabbitMQ Service... (1/7)
@echo off
start kubectl apply -f ..\K8S\1_rabbitmq-depl.yaml
timeout 40 >nul
echo -^> RabbitMQ Service implemented

echo -^> Installing Security MSSQL Service... (2/7)
@echo off
start kubectl apply -f ..\K8S\2_security-mssql-depl.yaml
timeout 90 >nul
echo -^> Security MSSQL Service implemented

echo -^> Installing Backend MSSQL Service... (3/7)
@echo off
start kubectl apply -f ..\K8S\3_backend-mssql-depl.yaml
timeout 60 >nul
echo -^> Backend MSSQL Service implemented

echo -^> Installing Security Service... (4/7)
@echo off
start kubectl apply -f ..\K8S\4_security-service-depl.yaml
timeout 30 >nul
echo -^> Security Service implemented

echo -^> Installing Backend Service... (5/7)
@echo off
start kubectl apply -f ..\K8S\5_backend-service-depl.yaml
timeout 30 >nul
echo -^> Backend Service implemented

echo -^> Installing Web Client... (6/7)
@echo off
start kubectl apply -f ..\K8S\6_web-client-depl.yaml
timeout 30 >nul
echo -^> Web Client implemented

echo -^> Installing Bot Service... (7/7)
@echo off
start kubectl apply -f ..\K8S\7_bot-service-depl.yaml
timeout 30 >nul
echo -^> Bot Service implemented

echo -^> -----Installation FINISHED-----
echo -^> Access the following address: http://localhost:30101/

pause