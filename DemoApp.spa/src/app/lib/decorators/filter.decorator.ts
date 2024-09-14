import 'reflect-metadata';

const metadataKey = Symbol('Filter');

export function Filter(): (target: object, propertyKey: string) => void {
  return registerProperty;
}

function registerProperty(target: object, propertyKey: string): void {
  let properties: string[] = Reflect.getMetadata(metadataKey, target);

  if (properties) {
    properties.push(propertyKey);
  } else {
    properties = [propertyKey];
    Reflect.defineMetadata(metadataKey, properties, target);
  }
}

export function getFilterProperties<T>(origin: T): object {
  const result = {};

  const properties: string[] = Reflect.getMetadata(metadataKey, origin);
  if (properties) {
    const propertiesWithFilter = properties.map((key) => `filter_${key}`);
    propertiesWithFilter.forEach((key) => (result[key] = origin[key]));
  }

  return result;
}
