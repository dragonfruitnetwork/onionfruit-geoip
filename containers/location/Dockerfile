FROM debian

RUN apt update && apt install curl software-properties-common ca-certificates gnupg2 -y

RUN echo "deb     https://packages.ipfire.org/location buster/" >> /etc/apt/sources.list.d/location.list
RUN echo "deb-src https://packages.ipfire.org/location buster/" >> /etc/apt/sources.list.d/location.list
RUN curl -fsSL https://packages.ipfire.org/79842AA7CDBA7AE3-pub.asc | apt-key add -

RUN apt update && apt install python3.7 && apt install location -y

CMD location update && location dump /app/out/dmp.txt
