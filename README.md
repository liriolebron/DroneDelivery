
# Drone Delivery

Efficient drone delivery trip calculation process.

# Approach used

The code expects a list of drones and delivery locations, each drone has the name and the maximum capacity it can carry:

> [DroneA][200], [DroneB][300], etc. 

Each delivery location has the location name and delivery weight: 

> [New York][50]

> [Florida][200], etc. 

The code permutes the drone collection, it means that it creates arrays with every possible combination of the drones to make sure the least amount of trips is generated. Afterwards, the code performs the delivery schedule for each combination and ouputs the drone combination that yielded the least amount of trips and was able to fulfill the delivery schedule.

## Dependencies

.NET 6 Desktop [Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) 

## Run Locally

Clone the project

```bash
  git clone https://github.com/liriolebron/DroneDelivery
```

Go to the repository location

```bash
  cd DroneDelivery
```

Navigate to the main project directory

```bash
  cd DroneDelivery
```

Execute the program

```bash
  dotnet run
```

## Running Tests

To run tests, first navigate to the tests directory

```bash
  cd DroneDeliveryTests
```

Run the tests

```bash
  dotnet test
```
