# Cloud Access Monitoring & Anomaly Detection
This project focuses on detecting unauthorised identity and access behaviour in cloud environments using log analysis and anomaly detection techniques.

## Overview
The system analyses cloud audit logs and access records to model normal user behaviour and identify suspicious or anomalous activity. It is designed to support proactive security monitoring and integration with SIEM platforms.

## Key Features
- Cloud log ingestion and preprocessing
- Feature engineering on access behaviour patterns
- Anomaly detection using statistical and ML-based techniques
- Event-driven alerting for suspicious access
- Designed for SIEM compatibility

## Tech Stack
- Python
- Cloud Audit Logs (AWS CloudTrail – simulated)
- Log preprocessing & feature engineering
- Anomaly detection techniques
- Event-driven monitoring concepts

## Project Structure
```text
src/
├── data_collection/
├── preprocessing/
├── feature_engineering/
├── anomaly_detection/
└── alerting/
```

## How It Works
1. Cloud access logs are collected and normalised
2. Relevant access features are extracted
3. User behaviour baselines are created
4. Anomalies are detected based on deviations
5. Alerts are generated for suspicious activity

## Notes
- Sensitive credentials and raw logs are intentionally excluded
- This project was developed as part of an academic research study and later cleaned for public sharing

## Author
Shivi Chauhan  
Backend Software Engineer  
