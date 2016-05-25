/* PROVIDER SCHEMA BLOCK -- VERSION 3 */

/* CREATE OUR APPLICATION AND USER TABLES */
CREATE TABLE MY_ASPNET_APPLICATIONS(ID INT PRIMARY KEY AUTO_INCREMENT, NAME VARCHAR(256), DESCRIPTION VARCHAR(256));
CREATE TABLE MY_ASPNET_USERS(ID INT PRIMARY KEY AUTO_INCREMENT, 
		APPLICATIONID INT NOT NULL, NAME VARCHAR(256) NOT NULL, 
		ISANONYMOUS TINYINT(1) NOT NULL DEFAULT 1, LASTACTIVITYDATE DATETIME);
CREATE TABLE MY_ASPNET_PROFILES(USERID INT PRIMARY KEY, VALUEINDEX LONGTEXT, STRINGDATA LONGTEXT, BINARYDATA LONGBLOB, LASTUPDATEDDATE TIMESTAMP);
CREATE TABLE MY_ASPNET_SCHEMAVERSION(VERSION INT);

INSERT INTO MY_ASPNET_SCHEMAVERSION VALUES (3);
 
/* NOW WE NEED TO MIGRATE ALL APPLICATIONS INTO OUR APPS TABLE */
INSERT INTO MY_ASPNET_APPLICATIONS (NAME) SELECT APPLICATIONNAME FROM MYSQL_MEMBERSHIP UNION SELECT APPLICATIONNAME FROM MYSQL_USERSINROLES;

/* NOW WE MAKE OUR CHANGES TO THE EXISTING TABLES */
ALTER TABLE MYSQL_MEMBERSHIP
          RENAME TO MY_ASPNET_MEMBERSHIP,
          DROP PRIMARY KEY,
          DROP COLUMN PKID,
          DROP COLUMN ISONLINE,
          ADD COLUMN USERID INT FIRST,
          ADD COLUMN APPLICATIONID INT AFTER USERID;
          
ALTER TABLE MYSQL_ROLES
          RENAME TO MY_ASPNET_ROLES,
          DROP KEY ROLENAME,
          ADD COLUMN ID INT PRIMARY KEY AUTO_INCREMENT FIRST,
          ADD COLUMN APPLICATIONID INT NOT NULL AFTER ID;
          
ALTER TABLE MYSQL_USERSINROLES
          DROP KEY USERNAME,
          RENAME TO MY_ASPNET_USERSINROLES,
          ADD COLUMN USERID INT FIRST,
          ADD COLUMN ROLEID INT AFTER USERID,
          ADD COLUMN APPLICATIONID INT AFTER ROLEID;

ALTER TABLE MY_ASPNET_MEMBERSHIP CONVERT TO CHARACTER SET DEFAULT;
ALTER TABLE MY_ASPNET_ROLES CONVERT TO CHARACTER SET DEFAULT;
ALTER TABLE MY_ASPNET_USERSINROLES CONVERT TO CHARACTER SET DEFAULT;

/* THESE NEXT LINES SET THE APPLICATION ID ON OUR TABLES APPROPRIATELY */          
UPDATE MY_ASPNET_MEMBERSHIP M, MY_ASPNET_APPLICATIONS A SET M.APPLICATIONID = A.ID WHERE A.NAME=M.APPLICATIONNAME;
UPDATE MY_ASPNET_ROLES R, MY_ASPNET_APPLICATIONS A SET R.APPLICATIONID = A.ID WHERE A.NAME=R.APPLICATIONNAME;
UPDATE MY_ASPNET_USERSINROLES U, MY_ASPNET_APPLICATIONS A SET U.APPLICATIONID = A.ID WHERE A.NAME=U.APPLICATIONNAME;

/* NOW MERGE OUR USERNAMES INTO OUR USERS TABLE */
INSERT INTO MY_ASPNET_USERS (APPLICATIONID, NAME) 
        SELECT APPLICATIONID, USERNAME FROM MY_ASPNET_MEMBERSHIP
        UNION SELECT APPLICATIONID, USERNAME FROM MY_ASPNET_USERSINROLES; 
          
/* NOW SET THE USER IDS IN OUR TABLES ACCORDINGLY */        
UPDATE MY_ASPNET_MEMBERSHIP M, MY_ASPNET_USERS U SET M.USERID = U.ID WHERE U.NAME=M.USERNAME AND U.APPLICATIONID=M.APPLICATIONID;
UPDATE MY_ASPNET_USERSINROLES R, MY_ASPNET_USERS U SET R.USERID = U.ID WHERE U.NAME=R.USERNAME AND U.APPLICATIONID=R.APPLICATIONID;

/* NOW UPDATE THE ISANONYMOUS AND LAST ACTIVITY DATE FIELDS FOR THE USERS */        
UPDATE MY_ASPNET_USERS U, MY_ASPNET_MEMBERSHIP M 
        SET U.ISANONYMOUS=0, U.LASTACTIVITYDATE=M.LASTACTIVITYDATE 
        WHERE U.NAME = M.USERNAME;

/* MAKE FINAL CHANGES TO OUR TABLES */        
ALTER TABLE MY_ASPNET_MEMBERSHIP
          DROP COLUMN USERNAME,
          DROP COLUMN APPLICATIONNAME,
          DROP COLUMN APPLICATIONID,
          ADD PRIMARY KEY (USERID);
          
/* NEXT WE SET OUR ROLE ID VALUES APPROPRIATELY */
UPDATE MY_ASPNET_USERSINROLES U, MY_ASPNET_ROLES R SET U.ROLEID = R.ID WHERE U.ROLENAME = R.ROLENAME AND R.APPLICATIONID=U.APPLICATIONID;

/* NOW WE MAKE THE FINAL CHANGES TO OUR ROLES TABLES */                    
ALTER TABLE MY_ASPNET_ROLES
          DROP COLUMN APPLICATIONNAME,
          CHANGE COLUMN ROLENAME NAME VARCHAR(255) NOT NULL;
          
ALTER TABLE MY_ASPNET_USERSINROLES
          DROP COLUMN APPLICATIONNAME,
          DROP COLUMN APPLICATIONID,
          DROP COLUMN USERNAME,
          DROP COLUMN ROLENAME,
          ADD PRIMARY KEY (USERID, ROLEID);