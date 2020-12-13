# NVD-NIST Parser
NVD-NIST Parser is a parser service. It parses to NVD source and information to you about your product vulnerabilities.

  - Get all affected vulnerabilities of your product.
  - Get vulnerabilitiy detail.


### Simple usage:
Now it service has only 2 endpoint. One of provides to get all vulnerabilities of your product.
Request url :
```sh
/api/GetAllVulnerabilities?vendorName=wordpress&productName=wordpress
```

Curl
```sh
curl -X GET "https://localhost:44306/api/GetAllVulnerabilities?vendorName=wordpress&productName=wordpress" -H  "accept: */*"
```
Response :
```sh
[
  "CVE-2020-28040",
  "CVE-2020-28039",
  "CVE-2020-28038",
  "CVE-2020-28037",
  "CVE-2020-28036",
  "CVE-2020-28035",
  .....
  .....
  .....
```
And other one is gettting vullnerabilities detail

Request url :
```sh
/api/GetVulnerabilityDetails?cve=CVE-2020-28040&vendorName=wordpress&productName=wordpress
```

Curl
```sh
curl -X GET "https://localhost:44306/api/GetVulnerabilityDetails?cve=CVE-2020-28040&vendorName=wordpress&productName=wordpress" -H  "accept: */*"
```
Response :
```sh
{
  "refers": "CVE-2020-28040",
  "title": "Cross-Site Request Forgery (CSRF)",
  "vendor": "wordpress",
  "product": "wordpress",
  "overview": "WordPress before 5.5.2 allows CSRF attacks that change a theme&#39;s background image.",
  "severity": "4.3 MEDIUM",
  "cvssMetric": "CVSS:3.1/AV:N/AC:L/PR:N/UI:R/S:U/C:N/I:L/A:N",
  "affectedVersions": [
    "-",
    "0.71",
    "0.71",
    "0.71",
    "0.71",
    "0.72",
    "0.72",
    "0.72",
    "0.711",
    "1.0",
    "1.0",
    "1.0",
    "1.0.1",
    "1.0.1",
    "1.0.1",
    "1.0.2",
    "1.1.1",
    "1.2",
    "1.2",
    "1.2",
    "1.2",
    "1.2",
    "1.2.1",
    "1.2.2",
    "1.2.3",
    "1.2.4",
    "1.2.5",
    "1.2.5",
    "1.3",
    "1.3.2",
    "1.3.3",
    "1.5",
    "1.5.1",
    "1.5.1.1",
    "1.5.1.2",
    "1.5.1.3",
    "1.5.2",
    "1.6.2",
    "2.0",
    "2.0",
    "2.0",
    "2.0.1",
    "2.0.1",
    "2.0.1",
    "2.0.2",
    "2.0.3",
    "2.0.3",
    "2.0.4",
    "2.0.5",
    "2.0.5",
    "2.0.5",
    "2.0.5",
    "2.0.6",
    "2.0.6",
    "2.0.6",
    "2.0.6",
    "2.0.6",
    "2.0.7",
    "2.0.7",
    "2.0.7",
    "2.0.7",
    "2.0.8",
    "2.0.8",
    "2.0.8",
    "2.0.9",
    "2.0.9",
     .....
     .....
  ]
}
```
