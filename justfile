# Default recipe to run when just is called without arguments
default:
    @just --list

# Build the project
build:
    dotnet build

# Run the project
run:
    dotnet run

# Clean build outputs
clean:
    dotnet clean
    rm -rf bin/ obj/

# Restore packages
restore:
    dotnet restore

# Apply database migrations
db-migrate:
    dotnet ef database update

# Create a new migration with the given name
db-migration name:
    dotnet ef migrations add {{name}}

# Remove the last migration
db-migration-remove:
    dotnet ef migrations remove

# Start services with Docker Compose
docker-up:
    docker-compose up -d

# Stop Docker Compose services
docker-down:
    docker-compose down

# Build and start services with Docker Compose
docker-rebuild:
    docker-compose up -d --build

# Show Docker Compose logs
docker-logs:
    docker-compose logs -f

# Run tests
test:
    dotnet test

# Watch for changes and restart the app when detected
watch:
    dotnet watch run

# Build Docker image
docker-build:
    docker build -t cinemaratona .

# Publish application
publish:
    dotnet publish -c Release -o ./publish

# Format code
format:
    dotnet format