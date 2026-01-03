# Cloud Access Monitoring & Anomaly Detection
This project focuses on monitoring unauthorized identity and access behavior in cloud-based systems by analyzing access logs and user activity patterns. It was developed as part of an academic research study and later cleaned and structured for public sharing.

## Overview
The system analyzes cloud audit logs and access records to model normal user behavior and identify suspicious or anomalous access patterns. The architecture emphasizes log ingestion, preprocessing, behavioral analysis, and extensibility for security monitoring use cases.

The project is designed with backend-driven monitoring concepts and aligns with SIEM-oriented workflows for proactive security analysis.

## Key Features
- Cloud access log ingestion and normalization
- Preprocessing and structuring of access data
- Behavioral analysis based on access patterns
- Backend-driven detection logic for suspicious activity
- Modular architecture aligned with monitoring systems
- Designed for SIEM integration and security visibility

## Tech Stack
- C# / ASP.NET Core
- Backend-driven monitoring architecture
- Cloud audit log concepts (AWS CloudTrail – simulated)
- Log preprocessing and behavioral analysis
- Event-driven monitoring principles
- Research-oriented system design

## Project Structure
```text
cloud-access-monitoring/
├── src/
│   ├── controllers/              # API controllers handling access and log ingestion
│   ├── models/                   # Data models representing users, access logs, and entities
│   ├── preprocessing/            # Data normalization and preprocessing logic
│   ├── extensions/               # Utility and extension methods (e.g., session helpers)
│   ├── ui/                       # UI views and frontend components
│   ├── migrations/               # Database migrations
│   ├── wwwroot/                  # Static assets
│   ├── properties/               # Application launch settings
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── Program.cs
│   ├── File_Access_Monitoring.csproj
│   └── README.md
│
├── .config/
│   └── dotnet-tools.json
```

## How It Works
1. Access and audit data is ingested through backend controllers
2. Raw data is preprocessed and normalized for consistency
3. User access behavior is modeled using structured data
4. Suspicious or abnormal patterns are identified through backend logic
5. The system supports alerting and integration with security monitoring tools

## Notes
- Sensitive credentials, secrets, and raw production logs are intentionally excluded
- This project was originally developed for academic research and later adapted for demonstration purposes
- Folder structure and naming have been cleaned for clarity and maintainability

## Author
Shivi Chauhan  
Backend Software Engineer  
