FROM rust:latest

RUN apt update && apt install git ca-certificates -y
RUN git clone https://github.com/torproject/tor.git --depth 1 /tor-source

WORKDIR /tor-source/scripts/maint/geoip/geoip-db-tool

RUN cargo build --release
CMD cargo run --release -- -i /data-in/dmp.txt -4 /data-out/geoip -6 /data-out/geoip6