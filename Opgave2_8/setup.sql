PRAGMA foreign_keys = ON;

DROP TABLE IF EXISTS "GroupUser";
DROP TABLE IF EXISTS "Group";
DROP TABLE IF EXISTS "User";

CREATE TABLE "User" (
    "UserId" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "Username" VARCHAR(32) NOT NULL,
    "Password" CHAR(36) NOT NULL
);

CREATE TABLE "Group" (
    "GroupId" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "Name" VARCHAR(32) NOT NULL
);

CREATE TABLE "GroupUser" (
    "GroupId" INT NOT NULL,
    "UserId" INT NOT NULL,
    CONSTRAINT "PK_GroupUser" PRIMARY KEY ("GroupId", "UserId"),
    CONSTRAINT "FK_GroupUser_Group_GroupId" FOREIGN KEY ("GroupId") REFERENCES "Group" ("GroupId"),
    CONSTRAINT "FK_GroupUser_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("UserId")
);

INSERT INTO "User" ("Username", "Password") VALUES
    ('Konrad', 'd43153fd-156f-4c9d-ac10-02e8db1eb99f'),
    ('Anne', 'bce3f4ca-5c60-49e9-b6a0-d0f2b4809832'),
    ('Steen', 'b5c4d3d0-c263-412f-9bb3-9756fc4fa8d3'),
    ('Remo', '0b261179-794e-4a9f-91b5-60d66d8d7651');

INSERT INTO "Group" ("Name") VALUES
    ('Bruger'),
    ('Administrator');

INSERT INTO "GroupUser" ("GroupId", "UserId") VALUES
    (1, 1),
    (2, 1),
    (1, 2),
    (2, 2),
    (1, 3),
    (1, 4);
