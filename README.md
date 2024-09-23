
# RedisCacheClient

RedisCacheClient is a library for managing cached objects using Redis in a .NET application. It provides functionalities to get, set, and delete cached objects, with support for hash and string data structures.

## Table of Contents

1. [Introduction](#introduction)
2. [Features](#features)
3. [Tech Stack](#tech-stack)
4. [Usage](#usage)
5. [Configuration](#configuration)

## Introduction

RedisCacheClient offers a seamless way to manage cached data using Redis, a popular in-memory data store. It allows you to efficiently retrieve and store data in Redis, supporting both key-value and hash-based data operations.

## Features

- **Get Cache Object:** Retrieve cached objects from Redis.
- **Set Cache Object:** Store objects in Redis with an optional expiration time.
- **Delete Cache Object:** Remove cached objects from Redis.
- **Hash Field Support:** Set and get objects from Redis hashes.

## Tech Stack

- **Backend:** .NET 8
- **Cache:** Redis
- **Dependency Injection:** Used for service registrations and configurations.

## Usage

1. **Register the RedisCacheClient:** Use the `RegisterRedisCacheClient` extension method to register the client in the dependency injection container.
2. **Configure the options:** Set up `RedisCacheClientOptions` with the necessary configuration, including the host, password, SSL, and expiry settings.
3. **Use the Client:** Use the `IRedisCacheClient` interface to interact with cached objects, including retrieving, storing, and deleting them.

## Configuration

### RedisCacheClientOptions

- **Host:** Redis server host.
- **Password:** Redis server password.
- **SSL:** Enable or disable SSL connection.
- **AbortOnConnectFail:** Whether to abort on connection failure.
- **DefaultStringExpiryDay:** Default expiration time for string keys.
- **DefaultHashExpiryDay:** Default expiration time for hash keys.

```csharp
public class RedisCacheClientOptions
{
    public string Host { get; set; }
    public string Password { get; set; }
    public bool Ssl { get; set; }
    public bool AbortOnConnectFail { get; set; }
    public int DefaultStringExpiryDay { get; set; }
    public int DefaultHashExpiryDay { get; set; }
}
```
