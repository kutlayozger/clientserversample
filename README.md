# Server Client Echo App

I prepared webserver app with python gevent. And I added keyboard before. And I prepared client (.net 6) and they work well on pc.

When I prepare Dockerfile keyboard does not work cause of dumpskeys not exists. (Keyboard hook not working)

I changed to libinput and again keyboard did not work in docker. (Both two solution works with on pc normally) 

I changed getch and problem solved. But getch paused server container and websocket side give a weird error.

I did not prepare compose yml. Dockerfiles are enough for sample. And I can create compose file with use dockerfiles.


For testing

On terminal 1 (for server side)
```
git clone --branch master https://github.com/kutlayozger/clientserversample
cd clientserversample/server
sh buildandrun.sh
```

On terminal 2 (for client side)
```
cd clientserversample/client
sh buildandrun
```
