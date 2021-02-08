# CommentManagement

##SetUp
Install : .NET Core 3.1, MongoDB and VS 2017 preferred

Once you install mongodb.  Please create a db with the name - comments. Create 3 collections 1.userInfo 2.messagethreads 3.post. Use the files from the root directory to import the data into collections

1. Clone the source code and open it in Visual Studio
2. Click on run or IIS Express
3. Open the postman import the collection - ThreadComments.postman_collection.json(part of source code and can be found i9n root directory)


##Features Implemented 
###1. GetComments() API with Pagination, DateRange, Keyword Search, Sort by latest or old. The same API supports in fetching replies to existing comments as well with all the features mwntioned
  This uses on demand loading concept. On the initial load only the Top/Root Comments are shown. On page scroll the older comments are auto fetched. Each request returns only 10 comments. Each Top/Root comment will have an option to view replies just like Facebook/IG or Reddit(on demand load or lazy load) which will fetch the replis to the comment but again only 10 at a time. And whi;le fetching the replies the existing filter values like Keywords, Date Range, Sort by Old etc are applied.  

###2. AddComment() Supports Creating new comments and Replying to existing comments

Databse Design:
I have desided to use Fan Out Write principle. Which means viewing/browsing will be fast but posting comments or replies will be slow. We can use pool of background tasks to handle the writes. 

I have decided to create 3 Collections - 1. User Collection(not need for this context), 2. Comments Collection 3. Post collection(post where the actual conversation is taking place)
To boost the performance, I am proposing sharding with PostId and UserName. This way we cqan even out the load on servers. But still to fetch the comments on initial load we can store 10 comments in the Post collection itself. So the initial load will be fast. And the firther request will be redirected to comments collection. With this the data will be duplicated and writing information will be slower. But there will always be a trade off and since posting comments will be very mych less compared to the read rate. 

Browsing - Best Performance (like browsing Instagram feed)
Replying/Posting comments-  Poor - Will be slower compared to browsing(like Uploading a photo in Instagram)
DataSize - Medium - Duplication of comments in comments and Post collection
