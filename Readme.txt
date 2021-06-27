To run the project : 

Open project in VS Code or Visual Studio

To build and run API project:
     1. cd .\API\TFNAPI
     2. dotnet watch run
     3. make a Postman POST request to the tfn endpoint: http://localhost:5000/TFN. Request payload : {TFN: "{tfn}"}}
            ex: http://localhost:5000/TFN {TFN: "111111111"}}
            
To install client app modules:
    1. open a new terminal
    2. cd .\client-app\
    3. npm install
    
To build client app:
    1. open a new terminal
    2. cd .\client-app\
    3. npm run build 

To run client app:
    1. open a new terminal
    2. cd .\client-app\
    3. npm start
    

