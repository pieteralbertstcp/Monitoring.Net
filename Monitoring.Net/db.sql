CREATE TABLE `monitoring_services` (
  `id` varchar(36) NOT NULL,
  `description` varchar(255) DEFAULT NULL,
  `class_name` varchar(50) DEFAULT NULL,
  `created_on` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_on` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Used to store a list of available monitoring services. Jobs will then be spawned of of this';


CREATE TABLE `monitoring_services_jobs` (
  `id` varchar(36) NOT NULL,
  `host` varchar(50) DEFAULT NULL,
  `username` varchar(50) DEFAULT NULL,
  `passwrd` varchar(50) DEFAULT NULL,
  `port` varchar(50) DEFAULT NULL,
  `display_name` varchar(50) DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  `cron` varchar(36) DEFAULT NULL,
  `execute_job` bit(1) DEFAULT 0,
  `monitoring_services_id` varchar(36) DEFAULT NULL,
  `notify_on_failure`  bit(1) DEFAULT 1,
  `created_on` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_on` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `monitoring_services_id` (`monitoring_services_id`),
  CONSTRAINT `monitoring_services_jobs_ibfk_1` FOREIGN KEY (`monitoring_services_id`) REFERENCES `monitoring_services` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Used to store a list of available monitoring services Jobs and their params.';


CREATE TABLE `monitoring_services_responses` (
  `id` varchar(36) NOT NULL,
  `host` varchar(50) DEFAULT NULL,
  `is_successful`  bit(1) DEFAULT 0,
  `passwrd` varchar(50) DEFAULT NULL,
  `port` varchar(50) DEFAULT NULL,
  `miliseconds_response` mediumtext,
  `message` varchar(255) DEFAULT NULL,
  `monitoring_services_jobs_id` varchar(36) DEFAULT NULL,
  `created_on` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_on` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `monitoring_services_jobs_id` (`monitoring_services_jobs_id`),
  CONSTRAINT `monitoring_services_responses_ibfk_1` FOREIGN KEY (`monitoring_services_jobs_id`) REFERENCES `monitoring_services_jobs` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Used to store a list of responses of the monitoring jobs.';


Insert into monitoring_services (id, description, class_name) values ('60a567d3-e1d0-48a0-a64a-5c69fffd75b2', 'Used to ping a hostname or IP address.','PingIp');
Insert into monitoring_services (id, description, class_name) values ('a78e6574-646f-4b62-825f-0bc021b8b314', 'Used to check if a FTP connection can be made to the desired host.','TestFtpConnection');
Insert into monitoring_services (id, description, class_name) values ('80eb0823-5253-4ded-bfcf-6a298416ebfd', 'Used to test if a website is alive. Checks for 200 status code.','IsWebsiteAlive');