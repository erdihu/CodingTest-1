#Assignment 
We work with a lot of bank information. To give you a sense of how that is we want you to 
create models for that kind of data. On the next page you will find some example data of a bank 
account and a credit card. A bank account contains events of types transactions and payments. 
Payments have a recipient number while transactions don’t. Credit cards only consist of credit 
card transactions. When you create the models have in mind that they should be​ extendable 
with other types of accounts and transactions. 

#Tasks 
1. Implement a model to represent the bank data. 
a. Model a credit card and its credit card transactions. 
b. Model a bank account and its bank events. 
2. Create a function that given several bank accounts with their bank events as input, 
removes all bank account transactions with a positive amount​ and outputs only the 
bank accounts that still have at least one event, also return the remaining events in the 
output. 
a. Create at least one unit test for this. 
3. Create a function that *calculates the balance* of a bank account or credit card. 
a. Create at least one unit test for this. 
4. Build a function to *detect the time interval* between subsequent bank events for a 
specific group (by text). You should be able to detect monthly and biweekly (every other 
week) intervals and represent that with appropriate data types. 
a. Create a unit test with the example bank account and detect that “Video 
streaming” occurs monthly. 
b. Create a unit test with the example bank account and detect that “Gym” occurs 
biweekly. The transaction at 2016.06.28 should be considered as noise. 

#Data
*Data available as data.json in the solution*
![Image of Data](https://vgy.me/xb5xdt.png)
