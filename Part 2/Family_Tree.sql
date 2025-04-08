 
----- 1 -----
 
 --CREATE DATABASE Family_Tree;

-- CREATE TABLE Person (
--    Person_Id INT PRIMARY KEY IDENTITY(1,1),
--    Personal_Name NVARCHAR(50) NOT NULL,
--	Family_Name NVARCHAR(50) NOT NULL,
--	Gender  INT CHECK (Gender IN (0, 1, 2)) , -- 0=Male, 1=Female, 2=Other 
--	Father_Id INT NULL,
--    Mother_Id INT NULL,
--    Spouse_Id INT NULL,
--    FOREIGN KEY (Father_Id) REFERENCES Person(Person_Id), 
--    FOREIGN KEY (Mother_Id) REFERENCES Person(Person_Id),
--    FOREIGN KEY (Spouse_Id) REFERENCES Person(Person_Id)
--);

--CREATE TABLE Family_Tree (
--	PRIMARY KEY (Person_Id, Relative_Id, Connection_Type),
--    Person_Id INT NOT NULL,
--    Relative_Id INT NOT NULL,
--	Connection_Type NVARCHAR(10) CHECK (Connection_Type IN ('אב','אם','אח', 'אחות', 'בן', 'בת', 'בן זוג', 'בת זוג')),
--	FOREIGN KEY (Person_Id) REFERENCES Person(Person_Id),
--    FOREIGN KEY (Relative_Id) REFERENCES Person(Person_Id)
--);



CREATE TABLE FAMILY_TREE (
    Person_Id INT NOT NULL,
    Relative_Id INT NOT NULL,
    Connection_Type NVARCHAR(20) NOT NULL
);


ALTER TABLE FAMILY_TREE
ADD CONSTRAINT FK_Family_Relative FOREIGN KEY (Relative_Id) REFERENCES Person(Person_Id);

ALTER TABLE FAMILY_TREE
ADD CONSTRAINT PK_FAMILY_TREE PRIMARY KEY (Person_Id, Relative_Id);


select * from Person
select * from Family_Tree

-----

INSERT INTO FAMILY_TREE (Person_Id, Relative_Id, Connection_Type)
SELECT p2.Person_Id, p1.Person_Id,
    CASE 
        WHEN p2.Father_Id = p1.Person_Id THEN N'אב'
        WHEN p2.Mother_Id = p1.Person_Id THEN N'אם'
    END
FROM Person p1
JOIN Person p2 ON p2.Father_Id = p1.Person_Id OR p2.Mother_Id = p1.Person_Id;


-----

INSERT INTO FAMILY_TREE (Person_Id, Relative_Id, Connection_Type)
SELECT Person_Id, Spouse_Id,
    CASE 
        WHEN p.Gender = 0 THEN N'בת זוג'
        WHEN p.Gender = 1 THEN N'בן זוג'
        ELSE N'בן/בת זוג'
    END
FROM Person p
WHERE Spouse_Id IS NOT NULL;



-----

INSERT INTO FAMILY_TREE (Person_Id, Relative_Id, Connection_Type)
SELECT 
    p1.Person_Id AS Parent_Id,
    p2.Person_Id AS Child_Id,
    CASE 
        WHEN p2.Gender = 0 THEN N'בן'
        WHEN p2.Gender = 1 THEN N'בת'
        ELSE N'בן/בת'
    END AS Connection_Type
FROM Person p1
JOIN Person p2 
    ON p2.Father_Id = p1.Person_Id OR p2.Mother_Id = p1.Person_Id;



-----

INSERT INTO FAMILY_TREE (Person_Id, Relative_Id, Connection_Type)
SELECT DISTINCT p1.Person_Id, p2.Person_Id,
    CASE 
        WHEN p2.Gender = 0 THEN N'אח'
        WHEN p2.Gender = 1 THEN N'אחות'
        ELSE N'אח/אחות'
    END
FROM Person p1
JOIN Person p2 
    ON p1.Person_Id <> p2.Person_Id
    AND (
        (p1.Father_Id IS NOT NULL AND p2.Father_Id IS NOT NULL AND p1.Father_Id = p2.Father_Id)
    OR
	    (p1.Mother_Id IS NOT NULL AND p2.Mother_Id IS NOT NULL AND p1.Mother_Id = p2.Mother_Id)
    );



----- 2 -----


INSERT INTO FAMILY_TREE (Person_Id, Relative_Id, Connection_Type)
SELECT 
    p1.Spouse_Id AS Person_Id, 
    p1.Person_Id AS Relative_Id, 
    CASE 
        WHEN p1.Gender = 0 THEN N'בן זוג'  
        WHEN p1.Gender = 1 THEN N'בת זוג' 
        ELSE N'בן/בת זוג'                 
    END AS Connection_Type
FROM Person p1
JOIN FAMILY_TREE ft
    ON ft.Person_Id = p1.Person_Id
WHERE p1.Spouse_Id IS NOT NULL   
    AND NOT EXISTS (
        SELECT 1 FROM FAMILY_TREE ft2
        WHERE ft2.Person_Id = p1.Spouse_Id
        AND ft2.Relative_Id = p1.Person_Id
    );

