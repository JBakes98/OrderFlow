#!/bin/zsh

URL="http://localhost:8080/orders"
INSTRUMENT_ID="09e6bd80-1904-4d82-9fa0-22e12f56c5c4"  # Replace with actual instrument ID
NUM_REQUESTS=100  # Number of requests to send

for i in {1..$NUM_REQUESTS}; do
  SIDE=$([ $((i % 2)) -eq 0 ] && echo "buy" || echo "sell")
  QUANTITY=$(( (RANDOM % 1000) + 1 ))  # Random quantity between 1 and 1000
  PRICE=$(printf "%.2f" "$(echo "scale=2; ($RANDOM % 500) + 10 + ($RANDOM % 100) / 100.0" | bc)")  # Random price between 10 and 510 with 2 decimals

  JSON_PAYLOAD=$(cat <<EOF
{
  "instrumentId": "$INSTRUMENT_ID",
  "quantity": $QUANTITY,
  "side": "$SIDE",
  "price": $PRICE
}
EOF
)

  curl -X POST "$URL" \
       -H "Content-Type: application/json" \
       -d "$JSON_PAYLOAD"

  echo "\nSent $SIDE order #$i with quantity $QUANTITY at price $PRICE"
  sleep 1  # Optional delay
done