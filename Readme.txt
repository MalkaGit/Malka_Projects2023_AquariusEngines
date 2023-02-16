================================================================
Overview
================================================================
1. IotSimulator	app	- fakes messages from iot and stores into kafka
2. IotAlertator	app	- consume messages from kafka.
					  when smaple is not valid, write alert to sql table
3. IotRestApi		- intercacting with db
					  used by ui app

4. IotUi			- the ui app

using:
	sql db with single table holding alerts
	kafka with single topic holding messages


================================================================
0. create sql table
================================================================
   sql db: Research
   sql table: 
		USE [Research]
		GO

		/****** Object:  Table [dbo].[AeAlerts]    Script Date: 2/15/2023 2:06:38 PM ******/
		SET ANSI_NULLS ON
		GO

		SET QUOTED_IDENTIFIER ON
		GO

	CREATE TABLE [dbo].[AeIotAlerts](
		[AlertId] [bigint] IDENTITY(1,1) NOT NULL,
		[SensorId] [int] NOT NULL,
		[SignalTime] [datetime] NOT NULL,
		[SignalValue] [float] NOT NULL,
		[SignalType] [int] NOT NULL,
		CONSTRAINT [PK_AeAlerts] PRIMARY KEY CLUSTERED 
		(
		[AlertId] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
		) ON [PRIMARY]
		GO

================================================================
docker running kafka
================================================================
1. install docker desktop
2. create docker-compose.yml

3. start docker 
	go into the directory with docker-compose.yml
	docker-compose up
4. see running dockers
	open new cmd to the directory of the docer-compose.yml
	docker-compose ps

5. to create topic called ae_messages
5.0 connect to bash
	docker ps			(see the processe)
	docker exec -it kafka sh 
	cd /opt/kafka/bin

5.1 list topics
	kafka-topics.sh --zookeeper zookeeper:2181 --list	

5.2 see how many partitions a topic has 
	kafka-topics.sh --zookeeper zookeeper:2181 --describe --topic theTopicName

5.3 create  topic called ae_messages
	kafka-topics.sh --create --zookeeper zookeeper:2181 --replication-factor 1 --partitions 1 --topic ae_messages
	

5.4 create  topic called ae_alerts
	kafka-topics.sh --create --zookeeper zookeeper:2181 --replication-factor 1 --partitions 1 --topic ae_alerts
	
5.5 run the command to see topics
	kafka-topics.sh --zookeeper zookeeper:2181 --list

6. to start conumer 

6.0 connect to bash
	docker ps			(see the processe)
	docker exec -it kafka sh 
	cd /opt/kafka/bin

6.1 list topics
	kafka-topics.sh --zookeeper zookeeper:2181 --list	


6.2 start consumer to topic called ae_messages (to see messages written to the topic)
	kafka-console-consumer.sh --bootstrap-server localhost:9092 --topic ae_messages --from-beginning

6.3 repear for topic ae_alerts
	

================================================================
start the rest api app: IotRestApi (used by ui app)
================================================================


================================================================
start simulator app (console application): itrSimulator
================================================================


================================================================
start alter generator app (console application): iotAlertator
================================================================


================================================================
start the ui app
================================================================










4.2 start producer
	OPTION1: kafka hashes message to select partition
               		kafka-console-producer.sh --broker-list localhost:9092 --topic test
		(for null key jut type value	for other key, type the key ad space and then the value)

	OPTION2: specify parition key
		https://stackoverflow.com/questions/26553412/how-to-produce-messages-to-selected-partition-using-kafka-console-producer
	
		kafka-console-producer.sh --broker-list localhost:9092 --topic test --property key.separator=,
		
		eg; 	34,{"PersonId":34, "FistNames":"Lizza"}




4.  (to stop docker instace:		docker-compose down
	OR
		docker ps
		docker stop <>
docker exec broker kafka-topics --bootstrap-server broker:9092  --create  --topic todel2201





