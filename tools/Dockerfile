FROM debian

RUN echo "deb     https://packages.ipfire.org/location buster/" >> /etc/apt/sources.list.d/location.list
RUN echo "deb-src https://packages.ipfire.org/location buster/" >> /etc/apt/sources.list.d/location.list

RUN apt update && apt install curl software-properties-common -y
RUN curl https://packages.ipfire.org/79842AA7CDBA7AE3-pub.asc | apt-key add -

RUN apt update && apt install location

RUN mkdir /app

WORKDIR /app
RUN echo "location update && location dump dmp.txt" >> script.sh

ENTRYPOINT ["script.sh"]
