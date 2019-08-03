# CsvFileParsing

This library is used to read data from csv file, extract data type of each column and returns data in a data table


## How to Use ##

**Create new Csv source**


/* filepath can be path of a local file as well as a file stored in a network location */
CsvSource source = new CsvSource("filePath");

/* If the file is in a network location and if to read the network user name and password is required */
CsvSource source = new CsvSource("filePath","userName","passord");

**Get data in C# data table**


DataTable table = source.Read();


## License ##
This is under free license.