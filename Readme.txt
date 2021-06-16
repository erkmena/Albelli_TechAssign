--Important Informations About Project--
In this project there are 2 options of db usage. One of them is SQLEXPRESS. For using this you need to have SQLEXPRESS on your pc also you need to set necessary configurations to work on Docker. If you don't have necessary configurations you can also work project on IIS EXPRESS.
Another choice is working on Azure DB. For working on this, first I have to add your IP to Azure DB Whitelist. So please, if you gonna use this, share your ip with me. I will add the ip immediately. After adding to whitelist you can basically change 
"DbChoice" value as "azure".

-About the Services-
For creating order you need to already have a card on db. So you need to create card first. And since card will need product types, you need to add necessary product types first.
After adding product types you can add card. In this step the mandatory attributes are quantity and productypeId in CardItem object. After modifying these attributes you can add your card.
After adding card you can add your order by modifying CustomerNameSurname attribute in order and CardId in Card which is sub-object of Order model.
For getting any object you only need related id of that object.