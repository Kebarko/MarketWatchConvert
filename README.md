# MarketWatch Convert

## Overview

Converts scraped data from the MarketWatch watchlist to the desired format.

## Requirements

```.NET 8.0```

## Installation

Unpack ```MarketWatchConvert.zip```.

## Usage

This application converts scraped data from the MarketWatch watchlist in a CSV format into a fixed defined format. It takes into account the status of the company (open/closed) and subtracts the difference if necessary.
Run ```MarketWatchConvert.exe``` with the path to the input file as a command line argument (or simply open the input file with this application). The program will create an output file with the same name as the input file and ```.txt``` extension and will also copy the output to the clipboard.

### Example

#### Input

Comma-separated values. Each company has one record.

| Column 1  | Column 2      | Column 3 | Column 4 |
|-----------|---------------|----------|----------|
| MSFT      | Last$414.57   | Chg2.25  | Open     |
| AMZN      | Last$189.42   | Chg-0.08 | Open     |
| YUMC      | Last$37.88    | Chg-0.22 | Open     |
| HK:1299   | LastHK$64.20  | Chg1.40  |          |
| DK:NOVO.B | Lastkr.883.20 | Chg0.60  |          |
| RUN       | Last$12.28    | Chg-0.14 | Open     |

#### Output

Tab-separated values. All companies are in one record.

|    |     |    |    |     |    |
|----|-----|----|----|-----|----|
|412 | 190 | 38 | 64 | 883 | 12 |

## License

MIT
