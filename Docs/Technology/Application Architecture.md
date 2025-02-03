# Application Architecture

Here we will list several core concepts about the architecture of the application we are building. The provided starter solution will already contain an example of the entire front-to-end setup. Watching the linked videos should give you all the information you need to get started. 

It's a lot of information though, don't forget to ask questions if something isn't clear!

# Modular Monolith
A modular monolith in software development is an architectural pattern that combines the simplicity of a traditional monolithic application with the benefits of modularity. In essence, it's a single application that is broken down into smaller, independent modules, each responsible for a specific function or feature of the overall application.

Here's what makes a modular monolith special:

1. Interchangeable Modules: Each module is designed to be independent and potentially reusable, meaning that different modules can be swapped out or updated without affecting the whole application.  
2. Improved Organization: By breaking down the application into modules, dependencies are more organized, making it easier to manage and understand which parts of the application depend on others.  
3. Code Reusability: Large development teams can reuse modules across projects, leading to faster and more consistent development. 
4. Observability: With clear boundaries between modules, it becomes easier to observe and debug the code, as you can focus on one module at a time. 
5.Less Complexity: Compared to microservices, which can be deployed independently and can use different technologies, a modular monolith is simpler to develop, deploy, and manage. 

A modular monolith offers a middle ground between the simplicity of a traditional monolith and the complexity of microservices. It allows for some of the benefits of microservices, like improved organization and code reusability, without the overhead of managing multiple services.

More information: https://medium.com/design-microservices-architecture-with-patterns/microservices-killer-modular-monolithic-architecture-ac83814f6862 or https://www.youtube.com/watch?v=z3piPJ7x4WU (the example in this video is a bit over-engineered but it's a very interesting video!)

# Vertical slicing
Vertical slicing in software architecture refers to the organization of code around specific features or use cases, rather than around technical concerns such as layers (e.g., UI, business logic, data access). Each feature, or "vertical slice," includes all aspects of the application needed to fulfill that feature, including the user interface, the business logic, and the data access layer.

Here's a simplified explanation:
1. Organization by Feature: Instead of separating code into layers like UI, business logic, and data access, vertical slicing groups code based on what feature it supports
2. High Cohesion: All the code related to a feature is located together, which enhances cohesion and makes it easier to understand and maintain
3. Reduced Coupling: While there is still some coupling within each slice, the goal is to minimize coupling between different slices 
Independent Development: Each slice can evolve independently, which is beneficial for agile development teams working on different features simultaneously

Here's an example of what a vertical slice might look like in a file structure:

📁 Features

    |__ 📁 UserRegistration

        |__ 📁 RegisterUser

            |__ 📁 UserCommand.cs

           |__ 📁 UserController.cs

              |__ 📁 rUserValidator.cs

              |__ 📁 UserRepository.cs


In this example, all the code related to user registration is contained within the UserRegistration slice, including the command, endpoint, handler, and validator.

Vertical slicing can be advantageous for larger, complex applications where features often require significant interaction with various parts of the system. By focusing on individual features, developers can work on a single aspect of the system at a time, potentially leading to more efficient development processes and less "cross-cutting" concern in the codebase.

More information: https://www.youtube.com/watch?v=lsddiYwWaOQ or https://www.youtube.com/watch?v=Ve__md8LeDY


# CQRS
CQRS, which stands for Command Query Responsibility Segregation, is a software architectural pattern that separates the operations of reading data (queries) from updating data (commands) within an application. Here's what it means in simpler terms:

* Commands: These are operations that make changes to the system. Think of adding a new product to a shopping cart or updating a user's profile. 
* Queries: These are operations that fetch data without altering anything. For instance, looking up product details or checking the status of an order. 
The CQRS pattern suggests that these two types of operations should be handled by different parts of the system, which helps to keep the code organized and focused. This separation allows for more efficient handling of read and write operations, which can be particularly beneficial in systems with high traffic or complex business rules.

Some of the advantages of using CQRS include:
* Scalability: By separating read and write operations, you can scale each part independently, which can lead to better performance and resource utilization. 
* Maintainability: The clear separation makes the code easier to understand and maintain, as different teams can work on the read and write sides without stepping on each other's toes. 
* Flexibility: The pattern allows for different optimization strategies for read and write operations, which can be tailored to the specific needs of the application. 

⚠️ CQRS does not mean event sourcing. This is a common misconception! ⚠️

More information: https://www.youtube.com/watch?v=vdi-p9StmG0


# Message Bus
The Wolverine library abstracts most of the message bus logic away, but it's important to understand how this works. Otherwise, it's a black box and can be very confusing.

When we start we will be using an in-application message bus. After some time we will migrate to Azure Service Bus.

More information: https://dev.to/billy_de_cartel/a-beginners-guide-to-understanding-message-bus-architecture-22ec


# Repository Pattern
The Repository Pattern is a design pattern used in software architecture to create an abstraction layer between the data access logic and the business logic of an application. It provides a way to decouple the way that data is retrieved and stored from the way that data is used within the application, making the code more maintainable, testable, and adaptable to changes in data sources.

Here's how it works:
* Abstraction of Data Access: The repository acts as a bridge between the application's business logic and the data storage. It provides a set of methods for performing CRUD (Create, Read, Update, Delete) operations on the data. By doing so, the application doesn't need to know the specifics of how data is stored or retrieved—it just interacts with the repository.
* Encapsulation of Data Access Logic: All the data access logic is encapsulated within the repository. This includes the code to connect to the database, execute SQL statements, handle transactions, etc. The business logic of the application remains unaware of these details.
Interface for Data Operations: The repository defines a well-defined interface that the rest of the application can use to interact with the data. This interface ensures that all data access is performed through a consistent set of methods, regardless of the underlying data storage technology.
* Simplified Data Manipulation: When you need to add, retrieve, update, or delete data, you use the repository's methods rather than writing raw SQL or using ORM calls. This simplifies the code and makes it easier to understand and maintain.
* Testing and Flexibility: Since the data access logic is isolated in the repository, it's easier to write tests for the business logic without having to deal with the database. Additionally, if you need to switch to a different type of database or data storage, you only need to change the implementation of the repository, not the business logic.

More information: https://www.youtube.com/watch?v=h4KIngWVpfU

