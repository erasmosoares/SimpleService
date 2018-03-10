# Diff Service
Diff service is an interface that accepts JSON base data to compare differences between two JSON files.

This solution was made using *Web Api* provided by .Net and *Entity Framework*.

<h2> Usage </h2>

- Clone repository {SimpleService} and open the solution in your visual studio.
- The project has a migration folder (Code first migration) that you can use to create the and seed the database. Since we are not talking about a production deploy, what should be done would only execute the following commands in the package manage console to make sure the database is ok.
```
Updata-Database
```
<h2> Endpoints </h2>

There is two ways for testing the service, once is using [Postman](https://www.getpostman.com/) that is the complete toolchain for API 
developers, or using a [simple windows client](https://github.com/erasmosoares/diffclient) created for testing this diff tool.

- This are the main URIs:

Post a data (JSON)

```
<host>/v1/diff/data/post
```

Headers | Body
------------ | -------------
Content-Type : application/json | {"File": "{\"name\":\"david\",\"age\":28,\"class\":\"mca\"}"

Get data ( Retrieve the list of stored JSONs)

```
<host>/v1/diff/data
```

Post a file into the right side of diff based into its {id}. 
```
/v1/diff/1/right
```
The service will respond with a plain text 

*" File 1 added to the right side "*

Post a file into the left side of diff based into its {id}. 
```
/v1/diff/2/left
```
The service will respond with a plain text 

*" File 2 added to the left side "*

Once you have the files into both sides, a diff can be performed using this URI
```
/v1/diff/1
```
The {id} means the side you want to see the results.

- If different sizes, the service will respond the files are different size.

- If same size, but different properties, the result show a text message like this >

Difference | Offsets / Line
------------ | -------------
different property value -> name : david | Found differences in line: 2

<h2> Improvements </h2>

This project represents a very basic API for finding differences between two JSON files. Some improvements would be interesting for
this API, just like better outputing results and deepest analysis for offsets { Line ; collums } differences. Also a web base client
with visual experiences.

# Client Side

Please check a [ windows base application ](https://github.com/erasmosoares/diffclient) available for testing the Diff Service.


