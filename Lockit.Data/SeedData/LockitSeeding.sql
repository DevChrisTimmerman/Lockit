-- Enable identity insert for Locations
SET IDENTITY_INSERT Locations ON;
-- Seed Locations
INSERT INTO Locations (ID, Name, Prefix, Description, LockerCount, LockersPerColumn) VALUES
(1, 'Main Hall', 'MH', 'Lockers near the main entrance', 20, 5),
(2, 'Science Wing', 'SW', 'Lockers in the science building', 15, 5),
(3, 'Library', 'Lib', 'Lockers near the library', 15, 5),
(4, 'Gym', 'G', 'Lockers in the gym area', 15, 5);
SET IDENTITY_INSERT Locations OFF;

-- Create a temporary table to store the generated student IDs
CREATE TABLE #TempStudents
(
    ID INT,
    Email VARCHAR(100)
);

-- Insert students and capture their generated IDs
INSERT INTO Students (Name, LastName, Email, Class, SCUID)
OUTPUT inserted.ID, inserted.Email INTO #TempStudents
VALUES
('Emma', 'Johnson', 'emma.johnson@school.com', '3B', 'SCU001'),
('Liam', 'Williams', 'liam.williams@school.com', '2A', 'SCU002'),
('Olivia', 'Brown', 'olivia.brown@school.com', '5C', 'SCU003'),
('Noah', 'Jones', 'noah.jones@school.com', '1D', 'SCU004'),
('Ava', 'Garcia', 'ava.garcia@school.com', '4A', 'SCU005'),
('Elijah', 'Miller', 'elijah.miller@school.com', '6B', 'SCU006'),
('Sophia', 'Davis', 'sophia.davis@school.com', '2C', 'SCU007'),
('Mason', 'Martinez', 'mason.martinez@school.com', '3A', 'SCU008'),
('Isabella', 'Rodriguez', 'isabella.rodriguez@school.com', '1B', 'SCU009'),
('Lucas', 'Hernandez', 'lucas.hernandez@school.com', '5D', 'SCU010'),
('Mia', 'Lopez', 'mia.lopez@school.com', '4C', 'SCU011'),
('Logan', 'Gonzalez', 'logan.gonzalez@school.com', '2B', 'SCU012'),
('Charlotte', 'Wilson', 'charlotte.wilson@school.com', '6A', 'SCU013'),
('Ethan', 'Anderson', 'ethan.anderson@school.com', '3D', 'SCU014'),
('Amelia', 'Thomas', 'amelia.thomas@school.com', '1C', 'SCU015'),
('Oliver', 'White', 'oliver.white@school.com', '4B', 'SCU016'),
('Aria', 'Hall', 'aria.hall@school.com', '2D', 'SCU017'),
('Henry', 'Clark', 'henry.clark@school.com', '5A', 'SCU018'),
('Luna', 'Lewis', 'luna.lewis@school.com', '3C', 'SCU019'),
('Jack', 'Young', 'jack.young@school.com', '1A', 'SCU020'),
('Nova', 'Walker', 'nova.walker@school.com', '4D', 'SCU021'),
('Leo', 'King', 'leo.king@school.com', '2C', 'SCU022'),
('Hazel', 'Wright', 'hazel.wright@school.com', '5B', 'SCU023'),
('Felix', 'Lopez', 'felix.lopez@school.com', '3B', 'SCU024'),
('Aurora', 'Hill', 'aurora.hill@school.com', '1D', 'SCU025'),
('Finn', 'Scott', 'finn.scott@school.com', '4A', 'SCU026'),
('Ruby', 'Green', 'ruby.green@school.com', '2B', 'SCU027'),
('Atlas', 'Adams', 'atlas.adams@school.com', '5D', 'SCU028'),
('Cora', 'Baker', 'cora.baker@school.com', '3A', 'SCU029'),
('Axel', 'Carter', 'axel.carter@school.com', '1C', 'SCU030'),
('Isla', 'Morris', 'isla.morris@school.com', '4C', 'SCU031'),
('Hugo', 'Price', 'hugo.price@school.com', '2D', 'SCU032'),
('Eden', 'Ross', 'eden.ross@school.com', '5A', 'SCU033'),
('Owen', 'Cole', 'owen.cole@school.com', '3D', 'SCU034'),
('Ivy', 'Ward', 'ivy.ward@school.com', '1B', 'SCU035'),
('Max', 'Fox', 'max.fox@school.com', '4B', 'SCU036'),
('Eve', 'Gray', 'eve.gray@school.com', '2A', 'SCU037'),
('Leo', 'Hunt', 'leo.hunt@school.com', '5C', 'SCU038'),
('Mae', 'West', 'mae.west@school.com', '3C', 'SCU039'),
('Ray', 'Nash', 'ray.nash@school.com', '1A', 'SCU040'),
('Joy', 'Day', 'joy.day@school.com', '4D', 'SCU041'),
('Kai', 'Shaw', 'kai.shaw@school.com', '2C', 'SCU042'),
('Ana', 'Boyd', 'ana.boyd@school.com', '5B', 'SCU043'),
('Rex', 'Page', 'rex.page@school.com', '3B', 'SCU044'),
('Zoe', 'Reid', 'zoe.reid@school.com', '1D', 'SCU045'),
('Ian', 'Hart', 'ian.hart@school.com', '4A', 'SCU046'),
('Amy', 'Lane', 'amy.lane@school.com', '2B', 'SCU047'),
('Ben', 'Hale', 'ben.hale@school.com', '5D', 'SCU048'),
('Sky', 'Moss', 'sky.moss@school.com', '3A', 'SCU049'),
('Tom', 'Kent', 'tom.kent@school.com', '1C', 'SCU050'),
('Mae', 'Bond', 'mae.bond@school.com', '4C', 'SCU051'),
('Sam', 'Wade', 'sam.wade@school.com', '2D', 'SCU052'),
('Ava', 'Tate', 'ava.tate@school.com', '5A', 'SCU053'),
('Oscar', 'Chen', 'oscar.chen@school.com', '3B', 'SCU054'),
('Lily', 'Zhang', 'lily.zhang@school.com', '4A', 'SCU055'),
('Gabriel', 'Cooper', 'gabriel.cooper@school.com', '2C', 'SCU056'),
('Sofia', 'Patel', 'sofia.patel@school.com', '5B', 'SCU057'),
('Lucas', 'Kim', 'lucas.kim@school.com', '1D', 'SCU058'),
('Maya', 'Singh', 'maya.singh@school.com', '4C', 'SCU059');

-- Enable identity insert for Lockers
SET IDENTITY_INSERT Lockers ON;

-- Insert Lockers using the generated student IDs from our temp table
INSERT INTO Lockers (ID, Number, StudentID, LocationID, Status)
SELECT 
    L.ID,
    L.Number,
    TS.ID,
    L.LocationID,
    L.Status
FROM (
    VALUES
    -- Main Hall Lockers (20 total)
    (1, 'MH01', 'emma.johnson@school.com', 1, 1),
    (2, 'MH02', 'liam.williams@school.com', 1, 1),
    (3, 'MH03', 'olivia.brown@school.com', 1, 1),
    (4, 'MH04', 'noah.jones@school.com', 1, 1),
    (5, 'MH05', 'ava.garcia@school.com', 1, 1),
    (6, 'MH06', 'elijah.miller@school.com', 1, 1),
    (7, 'MH07', 'sophia.davis@school.com', 1, 1),
    (8, 'MH08', 'mason.martinez@school.com', 1, 1),
    (9, 'MH09', 'isabella.rodriguez@school.com', 1, 1),
    (10, 'MH10', 'lucas.hernandez@school.com', 1, 1),
    (11, 'MH11', 'mia.lopez@school.com', 1, 1),
    (12, 'MH12', 'logan.gonzalez@school.com', 1, 1),
    (13, 'MH13', 'charlotte.wilson@school.com', 1, 1),
    (14, 'MH14', NULL, 1, 0), -- Available
    (15, 'MH15', 'amelia.thomas@school.com', 1, 1),
    (16, 'MH16', 'oliver.white@school.com', 1, 1),
    (17, 'MH17', 'aria.hall@school.com', 1, 1),
    (18, 'MH18', 'henry.clark@school.com', 1, 1), 
    (19, 'MH19', NULL, 1, 2), -- Maintenance
    (20, 'MH20', 'jack.young@school.com', 1, 1),

    -- Science Wing Lockers (15 total)
    (21, 'SW01', 'nova.walker@school.com', 2, 1),
    (22, 'SW02', 'leo.king@school.com', 2, 1),
    (23, 'SW03', 'hazel.wright@school.com', 2, 1),
    (24, 'SW04', 'felix.lopez@school.com', 2, 1),
    (25, 'SW05', 'aurora.hill@school.com', 2, 1),
    (26, 'SW06', 'finn.scott@school.com', 2, 1),
    (27, 'SW07', 'ruby.green@school.com', 2, 1),
    (28, 'SW08', 'atlas.adams@school.com', 2, 1),
    (29, 'SW09', 'cora.baker@school.com', 2, 1),
    (30, 'SW10', 'axel.carter@school.com', 2, 1),
    (31, 'SW11', NULL, 2, 0), -- Available
    (32, 'SW12', 'hugo.price@school.com', 2, 1),
    (33, 'SW13', 'eden.ross@school.com', 2, 1),
    (34, 'SW14', 'owen.cole@school.com', 2, 1),
    (35, 'SW15', 'ivy.ward@school.com', 2, 1),

    -- Library Lockers (15 total)
    (36, 'Lib01', 'max.fox@school.com', 3, 1),
    (37, 'Lib02', 'eve.gray@school.com', 3, 1),
    (38, 'Lib03', 'leo.hunt@school.com', 3, 1),
    (39, 'Lib04', 'mae.west@school.com', 3, 1),
    (40, 'Lib05', 'ray.nash@school.com', 3, 1),
    (41, 'Lib06', 'joy.day@school.com', 3, 1),
    (42, 'Lib07', NULL, 3, 0), -- Available
    (43, 'Lib08', 'ana.boyd@school.com', 3, 1),
    (44, 'Lib09', 'rex.page@school.com', 3, 1),
    (45, 'Lib10', 'zoe.reid@school.com', 3, 1),
    (46, 'Lib11', NULL, 3, 0), -- Available
    (47, 'Lib12', 'ian.hart@school.com', 3, 1),
    (48, 'Lib13', 'amy.lane@school.com', 3, 1),
    (49, 'Lib14', 'ben.hale@school.com', 3, 1),
    (50, 'Lib15', 'sky.moss@school.com', 3, 1),

    -- Gym Lockers (15 total)
    (51, 'G01', 'mae.bond@school.com', 4, 1),
    (52, 'G02', 'sam.wade@school.com', 4, 1),
    (53, 'G03', 'ava.tate@school.com', 4, 1),
    (54, 'G04', NULL, 4, 0), -- Available
    (55, 'G05', NULL, 4, 2), -- Maintenance
    (56, 'G06', 'isla.morris@school.com', 4, 1),
    (57, 'G07', 'kai.shaw@school.com', 4, 1),
    (58, 'G08', 'tom.kent@school.com', 4, 1),
    (59, 'G09', 'oscar.chen@school.com', 4, 1),
    (60, 'G10', 'lily.zhang@school.com', 4, 1),
    (61, 'G11', 'gabriel.cooper@school.com', 4, 1),
    (62, 'G12', 'sofia.patel@school.com', 4, 1),
    (63, 'G13', 'lucas.kim@school.com', 4, 1),
    (64, 'G14', 'maya.singh@school.com', 4, 1),
    (65, 'G15', 'ethan.anderson@school.com', 4, 1)
) AS L(ID, Number, Email, LocationID, Status)
LEFT JOIN #TempStudents TS ON L.Email = TS.Email;

SET IDENTITY_INSERT Lockers OFF;

-- Clean up
DROP TABLE #TempStudents;