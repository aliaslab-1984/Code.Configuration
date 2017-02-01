# Code.Configuration

The library provides a simple object based way to access _configuration_ values independent from the source and the encoding (eg.: base64, ciphered, ecc) of the values.

## Description

The library is based on the two abstract base calsses **AbstractConfigValue** and **AbstractConfigValueFactory**.

The second is a base factory which allows, if implemented, to retrieve a configuration value by name using one of the _GetValue_ methods which get a config value by name or deserializing a configuration protion of the library format (see section below).

The first provides the methods to serialize the referenced value in the library format, appling encoding trasformations and seamlessly casting the value to the desired (and supported format).

The library provides an implmentation of the class _AbstractConfigValueFactory_ (_BasicConfigValueFactory_) which is simply holds a collection of configuration value in a flyweight pattern fashion.

Two implementation of _AbstractConfigValue_ are provided also:

- _ConstantConfigValue_: is a named costant value.
- _AppSettingsConfigValue_ is a value read by the _appSettings_ section of a standard .NET .config file.

From the abstract base class each _AbstractConfigValue_ implementation inherit the application of every registered _value parser_. A value parser is a class implementing the _IValueParser_ interface:

<pre>
    public interface IValueParser
    {
        bool CheckSyntax(string value);

        string Parse(string value);
    }
</pre>

that can be registered to the value parsers collection:

<pre>
	IValueParser parser = new ...
	ValueParsers.AddParser(parser);
</pre>

and is applied to every _value_ during the _get_ operation:
 1. the _CheckSyntax_ method is evaluated against the string representation of the value
 2. if the check is positive, the parsing/transforamtion operation is performed on the value and is result is passed on (to the next value parser or as a result).

the unique _IValueParser_ implementation is **EncryptedValueParser** which verify if the value begins with _cipher:_ segment and applies a decrypt operation on the base64 encoded value (timmming the _cipher:_ part) using the algorithm AES with parameters the constant values _constant.config.chiper.IV_ and _constant.config.chiper.Key_ base64 encoded.

## Examples

<pre>
	<appSettings>
		<add key="val" value="3" />
		<add key="val_c" value="cipher:AAAAA=" /> <!-- 5 ciphered -->
		...

	public class Test
	{
		protected AbstractConfigValue val = new AppSettingConfigValue("val");
		protected AbstractConfigValue val_c = new AppSettingConfigValue("val_c");
		...

	...

	int x = val + 1 + val_c;
	bool res = x == 9; //true

</pre>

## Library serialized format

Each valuemust have:

- **type**: the type of the configuration object
- **name**: the unique name of the value
- **value**: the value of the configuration object if is constant
- **defaultValue**: the value of the configuration object if the value is missing
- **ref**: in alternative to **value** must have the **name** of another configuration object as a value

The unque serialization format implemented within the library is JSON.

Example:
<pre>
{
	type:"Code.Configuration.AppSettingsConfigValue",
	name:"val",
	defaultValue:0
}
...

{
	type:"Code.Configuration.ConstantConfigValue",
	name:"val",
	value:3
}
...
{
	type:"Code.Configuration.ConstantConfigValue",
	name:"val_1",
	ref:"val"
}
</pre>