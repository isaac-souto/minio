# Minio

Implementação de Minio Server gateway com Nginx como proxy reverso e api Asp.Net Core realizando a operação de criação do arquivo no bucket devolvendo a url de download.

# Inicialização de Ambiente

- Criar bucket S3
- Preencher o parâmetro MINIO_ACCESSKEY
- Preencher o parâmetro MINIO_SECRETKEY
- Preencher o parâmetro MINIO_BUCKET_FILES
- Preencher o parâmetro MINIO_ROOT_USER
- Preencher o parâmetro MINIO_ROOT_PASSWORD
- Executar **docker compose up**
- Realizar **POST** na url http://localhost:80
- http://localhost:9001 (Minio Console)

# SSL

- Criar pasta com os certificados
- Mapear o volume da pasta **etc/nginx/certs** no Nginx
- Mapear a porta **443** no Nginx
- Configurar arquivo **.conf** para ouvir a porta **443**

```
server {
    listen                  80;
    listen                  443 ssl;
    server_name             localhost;

    ssl_certificate         /etc/nginx/certs/certificate.crt;
    ssl_certificate_key     /etc/nginx/certs/private.key;
    ssl_trusted_certificate /etc/nginx/certs/certificate.crt;

    ...
}
```
