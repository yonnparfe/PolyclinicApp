## Running the Project with Docker

This project is a .NET Framework 4.8 application and requires Windows containers. The provided Docker setup builds and runs the application in a Windows environment using the official Microsoft .NET Framework 4.8 images.

### Project-Specific Requirements
- **.NET Framework Version:** 4.8 (as specified in the Dockerfile)
- **Windows Containers:** This project cannot run on Linux containers. Ensure Docker is set to use Windows containers.
- **No external services or databases** are configured by default. If you require a database, update the `docker-compose.yml` accordingly.

### Environment Variables
- No required environment variables are specified in the Dockerfile or compose file. If you need to use environment variables, add them to an `.env` file and uncomment the `env_file` line in `docker-compose.yml`.

### Build and Run Instructions
1. **Switch Docker to Windows containers** (if not already set).
2. **Build and start the application:**
   ```sh
   docker-compose up --build
   ```
   This will build the image using the provided `Dockerfile` and start the container as `csharp-polyclinicapp`.

### Special Configuration
- **GUI Applications:** This is a WPF/WinForms desktop application. Running GUI apps in containers requires desktop interaction, which is not supported on all hosts. Ensure your environment supports Windows containers with desktop interaction if you need to use the GUI.
- **User Security:** The container runs the application as a non-admin user (`appuser`) for improved security.

### Ports
- **No ports are exposed by default.**
- If you need to expose a port (e.g., for a web API), uncomment and configure the `ports` section in `docker-compose.yml`.

### Additional Notes
- If you add a database or other services, update the `docker-compose.yml` to include and configure them.
- For persistent data, add a `volumes` section as needed.

Refer to the `Dockerfile` and `docker-compose.yml` for further customization options specific to this project.