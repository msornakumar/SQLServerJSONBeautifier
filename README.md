# SQLServerJSONBeautifier
SQL Server JSON Beautifier \ Formatter  \ Indenter CLR function

SQL Server 2016 has the new feature of creating JSON output using FOR JSON clause similar to FOR XML path.The output of the above clause will not be formatted in a readable format.It will be returned as a one single string. To format the JSON , an external application like Visula Studio or Internet Online Formatting tools to be used. The purpose of this function is to format the JSON to a readable format in the SSMS output itself. This function beautifies the JSON and provides the output. This is a CLR Function created using C#.

### How Deploy

In the deploy folder open JSONBeautifier.sql and run the code in the same order. At the end of the script there are some examples provided on how to use this function.

### How to use the function

This is a scalar function which will take the JSON string as input and provide the formatted \ beautified \ intended JSON output.

#### Example 1

Pass the JSON value from a query into the function.

```
SELECT  [dbo].[IndentJSON](
		LTRIM
		(
		(
		SELECT TOP 2 A.ADDRESSID
					 ,A.CITY AS "REGION.CITY" 
					 ,A.COUNTRYREGION AS "REGION.COUNTRYREGION" 
		  FROM ADVENTUREWORKSLT2012.SALESLT.ADDRESS A
		 FOR JSON PATH , ROOT('Region')
		) 
		)
		)
```

#### Example 2

Pass the string directly in the function. This string can be assigned to a variable and passed as a parameter.

```
SELECT [dbo].[IndentJSON]('{"id": 1,"name": "A green door","price": 12.50,"tags": ["home", "green"]}')
```
