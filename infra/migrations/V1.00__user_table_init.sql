CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    given_name TEXT NOT NULL,
    middle_name TEXT NULL,
    family_name TEXT NOT NULL,
    email TEXT NOT NULL
);