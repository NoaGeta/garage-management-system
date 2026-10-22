# Garage Management System 🚗🔧

A robust, modular object-oriented application designed to manage garage repair workflows, vehicle servicing, and energy tracking across diverse vehicle types (fuel-based and electric cars, motorcycles, and trucks).

The project is structured with strict layer separation, isolating business logic into an independent Class Library (`.dll`) from the Console presentation layer (`.exe`).

## 🌟 Key Features
- **Strict Layer Separation:** Domain entities and operations reside entirely in a reusable assembly (`GarageLogic.dll`), with zero dependency on the UI layer.
- **Advanced OOP Architecture:** Full implementation of Polymorphism, Inheritance, Enums, and Generic Collections (`Dictionary<K, V>`, `List<T>`).
- **Dynamic Vehicle Management:** Handles diverse vehicle categories (Regular/Electric Cars, Regular/Electric Motorcycles, and Commercial Trucks) with unique attributes and constraints (tire PSI, fuel types, battery life).
- **Custom Exception Handling:** Built-in validation pipelines throwing targeted exceptions, including a custom domain exception (`ValueRangeException`) to enforce strict operational ranges.
- **Extensible Design:** Architecture allows integrating new vehicle types seamlessly via centralized factory instantiation routines.

## 🛠️ Tech Stack
- **Language & Runtime:** C#, .NET Framework
- **Architecture:** Layered Architecture, Factory Pattern, Custom Exception Handling
- **Collections:** `Dictionary<K, V>`, `List<T>`

## 📁 Solution Architecture
- `Ex03.GarageLogic/` - Independent Class Library (.dll) encapsulating all vehicle data models, domain logic, and validation rules.
- `Ex03.ConsoleUI/` - Console application (.exe) referencing the logic library, managing menus, and processing user input/output.

## 🚀 Getting Started
1. Clone the repository:
   ```bash
   git clone [https://github.com/NoaGeta/garage-management-system.git](https://github.com/NoaGeta/garage-management-system.git)
