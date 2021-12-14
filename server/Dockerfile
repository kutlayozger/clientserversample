FROM python

ENV PORT=8080

#RUN sudo apt-get update -y

#RUN apt-get -y install python3-pip

RUN pip install gevent
RUN pip install gevent-websocket
RUN pip install getch

COPY server.py /server.py

CMD ["python3", "/server.py"]
#CMD ["bash"]

EXPOSE 8080
