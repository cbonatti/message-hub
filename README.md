# message-hub
This project demonstrates real time comunication between multiple applications. It uses SignalR to handle the messages exchange. 
Once you input text on *Your input*  field it will dispatch the message and will update in all clients. Below *Your input*  field you will see a list of all connected clients and its respective messages. The list won't show the current client in the list, so if you open a web and a desktop client, both will display only one client in the list.

## Structure
### MessageBroker
Responsible for exchanging messages between consumers / producers.
### WebMvc
Web consumer / producer.
### WpfConsumer
Desktop consumer / producer.

## Getting Started
- Open **MessageBroker**  project on Visual Studio and start (make sure it runs on port 5123).
- Open **WebMvc** project on Visual Studio and start (browser will be automatically started).
- Open **WpfConsumer** project on Visual Studio and start.