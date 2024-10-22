#!/bin/zsh

# Create Apple instrument
curl -X POST http://localhost:8080/api/instrument \
-H "Content-Type: application/json" \
-d '{
  "ticker": "AAPL",
  "name": "Apple Inc.",
  "sector": "Technology",
  "exchange": "NASDAQ"
}'

# Create Google instrument
curl -X POST http://localhost:8080/api/instrument \
-H "Content-Type: application/json" \
-d '{
  "ticker": "GOOGL",
  "name": "Alphabet Inc.",
  "sector": "Technology",
  "exchange": "NASDAQ"
}'


# Create Amazon instrument
curl -X POST http://localhost:8080/api/instrument \
-H "Content-Type: application/json" \
-d '{
  "ticker": "AMZN",
  "name": "Amazon.com Inc.",
  "sector": "Consumer Cyclical",
  "exchange": "NASDAQ"
}'

# Create JPM instrument
curl -X POST http://localhost:8080/api/instrument \
-H "Content-Type: application/json" \
-d '{
  "ticker": "JPM",
  "name": "JPMorgan Chase & Co.",
  "sector": "Financial Services ",
  "exchange": "NYSE"
}'

# Create Coca-Cola instrument
curl -X POST http://localhost:8080/api/instrument \
-H "Content-Type: application/json" \
-d '{
  "ticker": "KO",
  "name": "The Coca-Cola Company",
  "sector": "Consumer Defensive",
  "exchange": "NYSE"
}'

# Create Tesla instrument
curl -X POST http://localhost:8080/api/instrument \
-H "Content-Type: application/json" \
-d '{
  "ticker": "TSLA",
  "name": "Tesla Inc.",
  "sector": "Consumer Cyclical",
  "exchange": "NASDAQ"
}'

# Create Spotify instrument
curl -X POST http://localhost:8080/api/instrument \
-H "Content-Type: application/json" \
-d '{
  "ticker": "SPOT",
  "name": "Spotify Technology S,A,",
  "sector": "Communication Services",
  "exchange": "NYSE"
}'

# Create Meta instrument
curl -X POST http://localhost:8080/api/instrument \
-H "Content-Type: application/json" \
-d '{
  "ticker": "META",
  "name": "Meta Platforms Inc.",
  "sector": "Communication Services",
  "exchange": "NASDAQ"
}'

# Create Microsoft instrument
curl -X POST http://localhost:8080/api/instrument \
-H "Content-Type: application/json" \
-d '{
  "ticker": "MSFT",
  "name": "Microsoft Corporation",
  "sector": "Technology",
  "exchange": "NASDAQ"
}'