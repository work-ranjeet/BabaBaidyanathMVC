USE BabaBaidyaNathMVC
GO

-------------------------------GroupType------------------------------------------------
INSERT INTO GroupsTypes (GroupsTypeID, GroupType, GroupDetails, IsActive)
VALUES (1, 'General', 'General users', 0)

INSERT INTO GroupsTypes (GroupsTypeID, GroupType, GroupDetails, IsActive)
VALUES (2, 'Admin', 'Admin users', 0)

-------------------------------Groups------------------------------------------------
INSERT INTO Groups (GroupsTypeID, IsActive)
VALUES (1, 1)

INSERT INTO Groups (GroupsTypeID, IsActive)
VALUES (2, 1)

-------------------------------informationType------------------------------------------------
INSERT INTO InformationType (InfTypeID, InfType, InfDetail, IsActive, IsDeleted)
VALUES (1, 'Tourism', 'Tourism about Deoghar', 1, 0)

INSERT INTO InformationType (InfTypeID, InfType, InfDetail, IsActive, IsDeleted)
VALUES (2, 'LatestPost', 'Latest Post by Jai', 1, 0)

-------------------------------ServiceTypes------------------------------------------------
INSERT INTO ServiceTypes (ServiceTypeID, ServiceType, ServiceDesp, IsActive)
VALUES (1, 'Services', 'Information about all puja', 1)


-------------------------------AlbumTypes------------------------------------------------
INSERT INTO AlbumTypes (AlbumTypeID, AlbumType, AlbumDesp, IsActive)
VALUES (1, 'Image', 'Imagaes of all image albums', 1)

INSERT INTO AlbumTypes (AlbumTypeID, AlbumType, AlbumDesp, IsActive)
VALUES (2, 'Video', 'Videos of all video albums', 1)
