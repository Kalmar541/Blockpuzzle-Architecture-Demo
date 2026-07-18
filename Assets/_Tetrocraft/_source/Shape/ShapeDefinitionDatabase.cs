using System;
using System.Collections.Generic;
using System.Linq;

namespace GameProject.Game
{
    public class ShapeDefinitionDatabase
    {
        public int Count => _database.Count;

        private Dictionary<ShapeType, ShapeDefinition> _database;
        private readonly Random _random = new();

        public ShapeDefinitionDatabase(ShapeDefinition[] definitions)
        {
            _database = new Dictionary<ShapeType, ShapeDefinition>();

            if (definitions == null || definitions.Length == 0)
                throw new ArgumentException("Definitions array cannot be null or empty");

            foreach (var definition in definitions)
            {
                if (definition == null)
                    throw new ArgumentNullException("ShapeDefinition cannot be null");

                if (_database.ContainsKey(definition.ShapeType))
                    throw new ArgumentException($"Duplicate definition for type: {definition.ShapeType}");

                _database.Add(definition.ShapeType, definition);
            }
        }

        public Position[] GetRotatedPositions(ShapeType shapeType, ShapeRotation rotation)
        {
            var definition = GetDefinition(shapeType);
            return definition.GetPositions(rotation);
        }

        public ShapeDefinition GetDefinition(ShapeType shapeType)
        {
            if (!_database.TryGetValue(shapeType, out var definition))
                throw new ArgumentException($"Unknown ShapeType: {shapeType}");

            return definition;
        }

        public ShapeDefinition GetRandomDefinition()
        {
            if (_database.Count == 0)
                throw new InvalidOperationException("ShapeDefinitionDatabase is empty!");

            var keys = _database.Keys.ToList();
            var randomKey = keys[_random.Next(keys.Count)];

            return _database[randomKey];
        }

        public bool HasDefinition(ShapeType shapeType)
        {
            return _database.ContainsKey(shapeType);
        }

        public IEnumerable<ShapeType> GetAllTypes()
        {
            return _database.Keys;
        }

        public IEnumerable<ShapeDefinition> GetAllDefinitions()
        {
            return _database.Values;
        }


    }
}