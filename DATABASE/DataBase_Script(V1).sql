-- 1. Crear la Base de Datos
CREATE DATABASE DataFlowHubDB;
GO

USE DataFlowHubDB;
GO

-- =============================================
-- NIVEL 1: CATÁLOGOS INDEPENDIENTES
-- =============================================

-- Tabla de Carreras
CREATE TABLE Majors (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Code NVARCHAR(10) NOT NULL
);

-- Tabla de Periodos Académicos
CREATE TABLE SchoolTerms (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    StartDate DATETIME2 NOT NULL,
    EndDate DATETIME2 NOT NULL,
    IsActive BIT NOT NULL -- 1=True, 0=False
);

-- Tabla de Salones
CREATE TABLE Classrooms (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    Location NVARCHAR(50) NULL,
    Capacity INT NOT NULL
);

-- Tabla de Profesores
CREATE TABLE Teachers (
    Id INT PRIMARY KEY IDENTITY(1,1),
    EmployeeNumber NVARCHAR(20) NOT NULL, -- El "Carnet" del profe
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20) NULL,
    HireDate DATETIME2 NOT NULL
);

-- =============================================
-- NIVEL 2: PERSONAS Y CURSOS (CON RELACIONES)
-- =============================================

-- Tabla de Estudiantes
CREATE TABLE Students (
    Id INT PRIMARY KEY IDENTITY(1,1),
    RegistrationNumber NVARCHAR(10) NOT NULL, -- El Carnet (231111)
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    DateOfBirth DATETIME2 NOT NULL,
    Address NVARCHAR(200) NULL,
    Phone NVARCHAR(20) NULL,
    MajorId INT NULL,
    
    -- Relación con Carreras
    CONSTRAINT FK_Students_Majors FOREIGN KEY (MajorId) REFERENCES Majors(Id)
);

-- Tabla de Usuarios (Login)
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL, -- Aquí va el Carnet o No. Empleado
    Password NVARCHAR(MAX) NOT NULL, -- Contraseña encriptada
    Role NVARCHAR(20) NOT NULL, -- 'Student', 'Teacher', 'Admin'
    StudentId INT NULL,
    TeacherId INT NULL,

    -- Relaciones opcionales
    CONSTRAINT FK_Users_Students FOREIGN KEY (StudentId) REFERENCES Students(Id),
    CONSTRAINT FK_Users_Teachers FOREIGN KEY (TeacherId) REFERENCES Teachers(Id)
);

-- Índice único para que no se repita el usuario
CREATE UNIQUE INDEX IX_Users_Username ON Users(Username);

-- Tabla de Cursos (Materias ofertadas)
CREATE TABLE Courses (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500) NULL,
    Credits INT NOT NULL,
    TeacherId INT NOT NULL,
    SchoolTermId INT NOT NULL,

    -- Relaciones
    CONSTRAINT FK_Courses_Teachers FOREIGN KEY (TeacherId) REFERENCES Teachers(Id),
    CONSTRAINT FK_Courses_SchoolTerms FOREIGN KEY (SchoolTermId) REFERENCES SchoolTerms(Id)
);

-- =============================================
-- NIVEL 3: DETALLES (HORARIOS)
-- =============================================

-- Tabla de Horarios
CREATE TABLE Schedules (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DayOfWeek NVARCHAR(20) NOT NULL, -- 'Monday', 'Tuesday'...
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    CourseId INT NOT NULL,
    ClassroomId INT NOT NULL,

    -- Relaciones
    CONSTRAINT FK_Schedules_Courses FOREIGN KEY (CourseId) REFERENCES Courses(Id),
    CONSTRAINT FK_Schedules_Classrooms FOREIGN KEY (ClassroomId) REFERENCES Classrooms(Id)
);

-- =============================================
-- NIVEL 4: TRANSACCIONAL (MATRÍCULAS Y PAGOS)
-- =============================================

-- Tabla de Matrículas
CREATE TABLE Enrollments (
    Id INT PRIMARY KEY IDENTITY(1,1),
    EnrollmentDate DATETIME2 NOT NULL,
    Status NVARCHAR(20) NULL, -- 'Active', 'Completed'
    StudentId INT NOT NULL,
    CourseId INT NOT NULL,

    -- Relaciones
    CONSTRAINT FK_Enrollments_Students FOREIGN KEY (StudentId) REFERENCES Students(Id),
    CONSTRAINT FK_Enrollments_Courses FOREIGN KEY (CourseId) REFERENCES Courses(Id)
);

-- Tabla de Transacciones Financieras
CREATE TABLE FinancialTransactions (
    Id INT PRIMARY KEY IDENTITY(1,1),
    TransactionDate DATETIME2 NOT NULL,
    Amount DECIMAL(18,2) NOT NULL, -- Dinero
    TransactionType INT NOT NULL, -- 1=Cargo, 2=Abono
    Description NVARCHAR(200) NULL,
    StudentId INT NOT NULL,

    -- Relaciones
    CONSTRAINT FK_FinancialTransactions_Students FOREIGN KEY (StudentId) REFERENCES Students(Id)
);

-- =============================================
-- NIVEL 5: NOTAS (DEPENDEN DE MATRÍCULA)
-- =============================================

-- Tabla de Notas
CREATE TABLE Grades (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL, -- 'Parcial 1'
    Value DECIMAL(5,2) NOT NULL, -- Nota ej: 85.50
    EvaluationDate DATETIME2 NOT NULL,
    EnrollmentId INT NOT NULL,

    -- Relaciones
    CONSTRAINT FK_Grades_Enrollments FOREIGN KEY (EnrollmentId) REFERENCES Enrollments(Id)
);
GO