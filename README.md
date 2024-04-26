# CLASSIFIED EMAIL FILTER

You have been hired by a company to write software to detect potential classified mail being exchanged
in non-secure email accounts. Your software will act as a filter to test the incoming email text, detect if
the email might be classified, and additionally replace any sensitive text with censored *****
characters. <br>
Please write the core code as a function that accepts as parameters: 
- a list of classified words/phrases 
- the email text 
The function should return:
- true/false flag - If any of the classified words or phrases were located in the email 
- censored text – a new email text where the classified words or phrases have been replaced with
asterisks, or the original email text if there was no classified material in the email
<br> 
Around the core function you can write a wrapper of your choice that lets a user input test data, run the
function, and see the result.
