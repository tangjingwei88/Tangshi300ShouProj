echo on

set Path=protogen.exe

%Path% -i:protoFile\ProtoMsg.proto -o:CsharpFile\ProtoMsg.cs

