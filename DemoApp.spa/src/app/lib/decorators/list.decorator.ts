import 'reflect-metadata';

const metadataKey = Symbol('List');
const listColumns: PropertyMetadata[] = [];

export function List(className?: string,columnName?: string): (target: object, propertyKey: string) => void {
  return (target: object, propertyKey: string) => {
    registerProperty(target, propertyKey, className, columnName);
  };
}

function registerProperty(target: object, propertyKey: string, className:string, columnName: string): void {
  let properties: string[] = Reflect.getMetadata(metadataKey, target);

  if (properties) {
    properties.push(propertyKey);
  } else {
    properties = [propertyKey];
    Reflect.defineMetadata(metadataKey, properties, target);
  }

  if(listColumns.filter(x => x.propertyKey === propertyKey)){
    listColumns.push({origin: className, propertyKey: propertyKey, columnName: columnName})
  }
}

export function getListProperties<T>(origin: T): object {
  const properties: string[] = Reflect.getMetadata(metadataKey, origin);
  const result = {};
  if (properties) {
    properties.forEach((key) => (result[key] = origin[key]));
  }
  return result;
}

export function getAdjustedColumnName<T>(origin:T, propertyKey: string): string {
    return listColumns.find(x => x.origin === origin.constructor.name && x.propertyKey === propertyKey ).columnName;
}

export class PropertyMetadata {
  origin: string;
  propertyKey: string;
  columnName: string;
}
