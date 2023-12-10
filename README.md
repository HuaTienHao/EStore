1. Open Visual Studio and chose "Clone a repository" and paste in "https://github.com/HuaTienHao/EStore.git"
![image](https://github.com/HuaTienHao/EStore/assets/132989526/d9f52c7f-e60d-49cd-b2bb-b02c967e7932)
2. Open appsetting.json and change the connection to your SQL Server's server name
![image](https://github.com/HuaTienHao/EStore/assets/132989526/16ead092-7683-42b3-a6c1-b31c22bee422)
3. Open Program.cs and uncomment this code (this code will create an admin user after running the program, only need to run once)
![image](https://github.com/HuaTienHao/EStore/assets/132989526/654ed7f9-1a99-4157-9ea1-263c159003a4)
4. Go to Tools -> NuGet Package Manager -> Package Manager Console, type "update-database" to the console and run it to create the database in your SQL Server
![image](https://github.com/HuaTienHao/EStore/assets/132989526/a75608d1-59f9-4137-a72e-b0fda3633ae4)
5. Run the program
6. Uncomment the code in Program.cs from step 3

The Admin user account is:
Email: admin@gmail.com
Password: Admin@123
