# Does currently not work with sonarr v4

# What alternatrr does
alternatrr lets you add alternative titles to your sonarr instance by editing the sonarr.db file directly via a simple UI.

# docker-compose.yaml
```yaml
# example docker-compose.yml

version: '3'
services:
  alternatrr:
    image: theultimatecoder/alternatrr:latest
    environment:
    - ConnectionStrings__DefaultConnection=Data Source=/opt/alternatrr/app/alternatrr.db
    - ConnectionStrings__sonarr=Data Source=/opt/sonarr/sonarr.db
    - Login__Username=Test
    - Login__Password=ChangeMe321!
    # Needed for reverseproxy
    - VIRTUAL_HOST=alternatrr.YOURDOMAIN.COM
    - VIRTUAL_PORT=80
    - LETSENCRYPT_HOST=alternatrr.YOURDOMAIN.COM
    - LETSENCRYPT_EMAIL=YOUREMAILADDRESS
    volumes:
    - /opt/sonarr:/opt/sonarr  #Mounting the directory with sonarr.db in it
    - /opt/alternatrr/app:/opt/alternatrr/app #Mounting the alternatrr.db peristent login
```
