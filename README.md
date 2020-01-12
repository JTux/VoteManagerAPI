# Vote Manager
Welcome to the Vote Manager API. It's a pretty simple API that allows the creation and simulation of "Sessions" where users may present motions that may be voted on. These motions may become Rules. Rules may be amended (through the voting process as well). You can find more details on the main components below.

With authorized user accounts and roles, the application's functionality can be limited depending on a user's current role.  
Roles that are currently defined include:
* Admin
* Chair
* Founder
* Member
* Observer

## Sessions
A Session is required for all of the core functionality to take place. When a Session is created, users may begin presenting Motions or Amendments to previously enacted Rules. There may only ever be one active Session at once, therefore if a Session is active all activity will be associated with that Session.

Current Session Functionality Includes:
* Creation (Admin and Chairperson)
* Get Current Active Session
* Get List of All Created Sessions
* Get a Session's Details by its ID
* Conclude the Session

Admins will also have the ability to entirely Delete a Session, which otherwise is not an option. Once something is added to the archives it cannot otherwise be removed. If it does not exist in the archives then it simply doesn't exist.  
Goals to add for Sessions include:
* Updating Session Info (if required)
* Build an endpoint that returns just the Motions or Amendments
* Calling a recess (Pausing the Session)
* Resuming a paused Session

<br>

## Motions
A Motion is pretty straight forward. When a Session is active, registered users (outside of the Observer role) will be allowed to present (Create) Motions. A Motion will have a Title and Description.

Users will be allowed to Vote on Motions. When a Vote on a Motion is concluded, votes are counted and if the majority is in favor, the Motion will be converted to a Rule.

Current Motion Functionality Includes:
* Creation
* Getting Motion Detail by its ID
* Getting All Motions
* Updating an Existing Motion (This removes all Votes associated with the Motion)
* Toggling the Motion's Table status
* Getting all Votes on the Motion
* Concluding the Motion
* Deleting (Admin Only)

<br>

## Votes
Votes are also pretty straight forward. When Motions (and later Amendments) are active, they may be voted on. Each voting eligible may cast one vote per Order Of Business (Motions and Amendments).

Votes have 4 different values:
* Aye
* Nay
* Present
* NoVote

<br>

## Rules/Enactments
When a vote on a Motion, if the vote passes then the Motion will be attached to a Rule/Enactment. These will be available for all to see.

Rules/Enactments can also change over time with the addition of Amendments. During sessions members may present Amendments that will be attached to enacted Rules. These can be voted on the same way that Motions can be.

<br>

## Amendments
An Amendment is a lot like a Motion, except it's only added on to existing Rules. When a Session is active, registered users (outside of the Observer role) will be allowed to present (Create) Amendments on existing Rules. An Amendment will have a Title and Description.

Users will be allowed to Vote on Amendments. When a Vote on a Amendment is concluded, votes are counted and if the majority is in favor, the Amendment added to the target Rule.

Current Amendment Functionality Includes:
* Creation
* Getting Amendment Detail by its ID
* Getting All Amendments
* Updating an Existing Amendment (This removes all Votes associated with the Amendment)
* Toggling the Amendment's Table status
* Getting all Votes on the Amendment
* Concluding the Amendment
* Deleting (Admin Only)