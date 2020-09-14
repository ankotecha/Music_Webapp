# Music_Webapp

SWARAGANGA

Swarganga is a powerful and elegant music player web app hosted on azure. We developed this web app using Asp.Net MVC ,Javascript,Ajax technologies and some external library like asp.net identity,smtp,html speech recoginition.

FEATURES:
•	Favourite Songs.
•	Voice Based Search For Album,Artist and Songs. 
•	Playlist Management
•	Trending Songs.
•	Recently Played Songs.
•	Login Registration via email confirmation.
•	Reset Password.


USER:
User can register and login.User can see recently played songs and trending songs and also can see favourite songs and playlist created by them.User can also reset password.User can search (also voice based) for artist,album and songs.

ADMIN:
Admin can manage users,upload new songs,edit songs,delete songs.

FLOW:
Our Homepage is DisplaySong view of SongsController which is set as default page in RouteConfig.cs file in App_Start folder.User can see trending songs,recently played songs and all songs on home page.after that user can create playlist and add song to playlist and add it to favourite songs.SongsController contain action method for FavoriteSong,Playlist,DisplaySongs.
User can register,confirm email address,login and reset password for which there are an action methods and views like register,login,confirmemail in AccountController.
Admin can create,update and delete user account and also edit,upload,delete songs for which there is an AdminUserController.
There is an authentication and authorization for admin and end user.

CONTRIBUTOR INFORMATION:

Name: Kotecha Ashish
Roll No: CE065
Id: 17CEUOS066

Name: Moradiya Harsh
Roll No: CE073
Id: 17CEUOG056

