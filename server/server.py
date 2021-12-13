import os
import gevent
from gevent.pywsgi import WSGIServer
from gevent.lock import Semaphore
from geventwebsocket.handler import WebSocketHandler
import keyboard

key = ""
port = os.getenv("PORT")
if port == None:
    port = 8080
else:
    try:
        port = int(port)
    except:
        port = 8080

def process(ws,data,sem):
    gevent.sleep(0.1)
    with sem:
        ws.send(data)


def onkeypress(ev):
    global key
    if (len(ev.name) == 1):
        key = ev.name

keyboard.on_press(onkeypress)

def app(environ, start_response):
    global key
    ws = environ['wsgi.websocket']
    sem = Semaphore()

    print("New client connected")
    while True:
        if (key != ""):
          print(">", key)
          gevent.spawn(process, ws, key, sem)
        key = ""
        gevent.sleep(0.1)

print("serverapp started listening", port)
server = WSGIServer(("", port), app, handler_class=WebSocketHandler)
server.serve_forever()
