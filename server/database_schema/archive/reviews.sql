CREATE TABLE Reviews (
    ReviewID INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT NOT NULL,
    ClothingID INT NOT NULL,
    Rating INT NOT NULL,
    Comment TEXT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (ClothingID) REFERENCES Clothing(ClothingID),
    CHECK (Rating >= 1 AND Rating <= 5)
);