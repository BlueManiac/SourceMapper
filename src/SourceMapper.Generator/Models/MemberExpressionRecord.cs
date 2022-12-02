using System;

namespace SourceMapper.Generator.Models;
public class MemberExpressionRecord : IMemberRecord
{
    public string Target { get; set; }
    public string Type { get; set; }
    public Func<string, string> SourceExpression { get; set; }
}
