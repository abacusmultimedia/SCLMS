#Project Overview
The Simple Cloud License Management System (S-CLMS) is a web application designed to manage, distribute, and validate licenses for cloud-based software products. The project emphasizes test-driven development to ensure robust and reliable functionality.

#Technologies Used
Backend: .NET Core (C#)
Frontend: Vue.js
Database: MS SQL
Version Control: Git

#Features 
User Authentication
Simple login system for Admin and Users.
Unit tests to verify registration and login functionality.

#Product Registration
Admin can add new software products, defining product name, version, and description.
License Generation & Distribution
Admin can generate license keys for specific products.
Licenses can be sent to registered users via email.
Unit tests to ensure accurate license generation and distribution.
License Activation & Validation
Users can activate their licenses by entering the license key.
Backend should validate the license.
Unit tests for license activation and validation mechanisms.
License Management Dashboard
Admin can view all generated licenses, their associated users, and their activation status.
