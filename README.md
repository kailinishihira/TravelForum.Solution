# _Travel Forum_

#### _A web app to track bands and the venues where they've performed, 08.25.17_

#### By _**Lois Choi, Charlie Kelson, Robert Murray, Kaili Nishihira, Michael Woldemedihin**_

## Description

_This web app enables a user to create a post about the places they visited an be able comment on others post_

|| Behavior  | Input  | Output  |
|---|---|---|---|
|1.| User may view a list of all posts from the index view.  |  |  |
|2.| User may click `Add post` and should be redirected to the add form |Click `Add Post`| user redirected to the post form |
|3.| User may view a posts's details by clicking the post title from the index view.  | Click `What to see in Bangkok`  | The post details for Bangcok will be displayed.  
|4.| User fills the form and click `Submit` the user should see the summary of the form and prompted to update or continue | Fill the form and Click `Add`| Summary of the form the user filled out and prompt buttons <br> `Update` or <br> `Create Post` |
|5. |When user clicks `Update` button, they will be redirected to prepopulated form to make a change on any field and with `Save` or `Cancel` buttons.| Click `Update`| prepopulated form <br> `Save` button <br> `Cancel` |
|6.| When user makes change to the field and clicks `Save` the data should be updated and the user should be redirected to the form summary view with the `Cancel` and `Update` button| Make a change and click `Save` |Redirected to form summary with the updated information.  |
|7. |When user clicks `Cancel` button, they will be redirected to list of posts view.| Click `Cancel`|Redirected to index view |
|8. |From the post's details view, the user should be able to `Update` or `Delete` a post| >Click `Update` <br> >Click `Delete`|> Redirected to the update view. <br> >Redirected to `Delete view` |
|9. |From the post's details view, the user can `Reply` to a post| Type a comment and Click `Reply` | The comment should be Submitted and should appear on the post.
|10. |From the post's details view, the user can `Update` their comment.| Click `Update` in front of a comment.| Redirected to prepopulated `Update comment` view and `Save` and `Cancel` button.
|11. |From the `Update comment` view, the user can `edit` their previous comment and save to the database.| >Click `Save` <br> >`Cancel`| >Save the changes and redirected to `post details` page <br> > Redirected to the `post details` page
|12. |From the `post details` view use can delete their comment.| Click `Delete`infront of the comment.| Redirected to `Delete view`

![](travel-schema.png)



## Setup/Installation Requirements

* _Download and install [.NET Core 1.1 SDK](https://www.microsoft.com/net/download/core)_
* _Download and install [Mono](http://www.mono-project.com/download/)_
* _Download and install [MAMP](https://www.mamp.info/en/)_
* _Set MySQL Port to 8889_
* _Clone repository_

#### There are two options to create the database:
##### 1. In MySQL
`> CREATE DATABASE travel_forum;`<br>
`> USE travel_forum;`<br>
`> CREATE TABLE regions (id serial PRIMARY KEY, name VARCHAR(255));`<br>
`> CREATE TABLE countries (id serial PRIMARY KEY, name VARCHAR(255));`<br>
`> CREATE TABLE cities (id serial PRIMARY KEY, name VARCHAR(255), country_id INT;`<br>
`> CREATE TABLE tags (id serial PRIMARY KEY, name VARCHAR(255));`<br>
`> CREATE TABLE replies (id serial PRIMARY KEY, name VARCHAR(255), text VARCHAR(255), post_id INT);`<br>
`> CREATE TABLE posts (id serial PRIMARY KEY, title VARCHAR (255), name VARCHAR(255), start_date DATETIME, end_date DATETIME, region_id INT, country_id INT, city_id INT, text VARCHAR (255));`<br>
`> CREATE TABLE posts_tags (id serial PRIMARY KEY, post_id INT, tag_id INT);`<br>

##### 2. Import from the Cloned Repository
* _Click 'Open start page' in MAMP_
* _Under 'Tools', select 'phpMyAdmin'_
* _Click 'Import' tab_
* _Choose database file (from cloned repository folder)_
* _Click 'Go'_

## SQL Design
![](/sql-design.png)

## Technologies Used
* _C#_
* _.NET_
* _[Bootstrap](http://getbootstrap.com/getting-started/)_
* _[MySQL](https://www.mysql.com/)_

### License

Copyright (c) 2017 **Lois Choi, Charlie Kelson, Robert Murray, Kaili Nishihira, Michael Woldemedihin**

*Licensed under the [MIT License](https://opensource.org/licenses/MIT)*
